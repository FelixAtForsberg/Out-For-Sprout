using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_Instantiator : MonoBehaviour
{
    //Event Vars
    public FMODUnity.EventReference evRef;
    private FMOD.Studio.EventInstance evInst;
    public bool is3D = false;
    public GameObject pos3D_Override;
    public bool playOnStart = false;
    public bool dontStopOnDestroy = false;

    //Param Vars
    private List<FMOD.Studio.PARAMETER_ID> paramIDs = new List<FMOD.Studio.PARAMETER_ID>();
    public List<string> paramNames = new List<string>();

    public bool printLengthOnStart = false;

    void Start()
    {
        //Create Instance
        evInst = FMODUnity.RuntimeManager.CreateInstance(evRef);

        //3D
        if (pos3D_Override == null) pos3D_Override = gameObject;
        if (is3D) FMODUnity.RuntimeManager.AttachInstanceToGameObject(evInst, GetComponent<Transform>(), GetComponent<Rigidbody>());

        //Param Setup        
        for (int i = 0; i < paramNames.Count; i++)
        {
            paramIDs.Add(generateID(paramNames[i], evInst));
        }

        //Start Event
        if (playOnStart) evInst.start();

        if (printLengthOnStart) Debug.Log("Event " + evRef.ToString() + " " + getEventLength());
    }

    public void setParam(string _name, float _val)
    {
        bool foundParam = false;

        for (int i = 0; i < paramNames.Count; i++)
        {
            if (_name == paramNames[i])
            {
                if (paramIDs.Count > 0) evInst.setParameterByID(paramIDs[i], _val);
                else evInst.setParameterByName(_name, _val); //Failsafe for when ID list hasn't finished populating when setParam is called...
                foundParam = true;
                break;
            }
        }

        if (!foundParam) Debug.Log("AUDIO: NO MATCHING PARAM NAME");
    }

    public void playEvent()
    {
        if (is3D) FMODUnity.RuntimeManager.AttachInstanceToGameObject(evInst, GetComponent<Transform>(), GetComponent<Rigidbody>());
        evInst.start();
    }

    public void playNewInstanceAndRelease(string paramName = "", float paramVal = 0f)
    {
        FMOD.Studio.EventInstance nInst = FMODUnity.RuntimeManager.CreateInstance(evRef);
        if (is3D) FMODUnity.RuntimeManager.AttachInstanceToGameObject(nInst, GetComponent<Transform>(), GetComponent<Rigidbody>());
        if (paramName != null) nInst.setParameterByName(paramName, paramVal);
        nInst.start();
        nInst.release();
    }

    public void stopEventWithFade()
    {
        evInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //evInst.release(); //ADD GARBAGE COLLECTION TO LOOPING EVENTS!!!
    }

    public void stopAndRelease()
    {
        evInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        evInst.release();
    }

    public float getEventLength()
    {
        int length = 0;
        FMOD.Studio.EventDescription eventDescription;
        evInst.getDescription(out eventDescription);
        eventDescription.getLength(out length);
        return length / 1000f;
    }

    private FMOD.Studio.PARAMETER_ID generateID(string _name, FMOD.Studio.EventInstance _evInst)
    {
        FMOD.Studio.EventDescription eventDescription;
        _evInst.getDescription(out eventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription;
        eventDescription.getParameterDescriptionByName(_name, out parameterDescription);
        return parameterDescription.id;
    }


    void OnDestroy()
    {
        //Failsafe & Garbage Collect
        if (dontStopOnDestroy) evInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        evInst.release();
    }
}
