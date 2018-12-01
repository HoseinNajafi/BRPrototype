using UnityEngine;
using System.Collections;
using Bolt;

public class CubeBehavior : EntityEventListener<ICubeState>
{
    public GameObject[] WeaponObjects;

    float resetColorTime;
    Renderer renderer;

    public override void Attached()
    {
        renderer = GetComponent<Renderer>();
        state.SetTransforms(state.CubeTransform, transform);

        if (entity.isOwner)
        {
            state.CColor = new Color(Random.value, Random.value, Random.value);
            for (int i = 0; i < state.WeaponArray.Length; ++i)
            {
                state.WeaponArray[i].WeaponId = i;
                state.WeaponArray[i].WeaponAmmo = Random.Range(50, 100);
            }

            state.WeaponActiveIndex = -1;
        }

        state.AddCallback("CColor", ColorChanged);

        state.AddCallback("WeaponActiveIndex", WeaponActiveIndexChanged);
    }

    private void WeaponActiveIndexChanged()
    {
        for (int i = 0; i < WeaponObjects.Length; ++i)
        {
            WeaponObjects[i].SetActive(false);
        }

        if (state.WeaponActiveIndex >= 0)
        {
            int objectId = state.WeaponArray[state.WeaponActiveIndex].WeaponId;
            WeaponObjects[objectId].SetActive(true);
        }
    }

    public override void OnEvent(FlashColorEvent evnt)
    {
        resetColorTime = Time.time + 0.2f;
        renderer.material.color = evnt.FlashColor;
    }

    void ColorChanged()
    {
        GetComponent<Renderer>().material.color = state.CColor;
    }

    void OnGUI()
    {
        if (entity.isOwner)
        {
            GUI.color = state.CColor;
            GUILayout.Label("@@@");
            GUI.color = Color.white;
        }
    }

    public override void SimulateOwner()
    {
        var speed = 4f;
        var movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) { movement.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movement.z -= 1; }
        if (Input.GetKey(KeyCode.A)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { movement.x += 1; }

        if (Input.GetKeyDown(KeyCode.Alpha1)) state.WeaponActiveIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) state.WeaponActiveIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) state.WeaponActiveIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha0)) state.WeaponActiveIndex = -1;

        if (movement != Vector3.zero)
            transform.position = transform.position + (movement.normalized*speed*BoltNetwork.frameDeltaTime);

        if (Input.GetKeyDown(KeyCode.F))
        {
            var flash = FlashColorEvent.Create(entity);
            flash.FlashColor = Color.red;
            flash.Send();
        }
        
    }

    void Update()
    {
        if (resetColorTime < Time.time)
        {
            renderer.material.color = state.CColor;
        }
    }


}