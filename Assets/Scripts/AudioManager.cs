using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public EventReference coffeeEvent;
    public string[] coffeeTexts;
    public EventReference printerEvent;
    public string[] printerTexts;
    public EventReference postEvent;
    public string[] postTexts;

    public void PlayOneShot(EventReference eventRef, Vector3 position, int value) {
        var instance = FMODUnity.RuntimeManager.CreateInstance(eventRef);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
        instance.setParameterByName("RequestType", value);
        instance.start();
        instance.release();
    }

    public string PlayRequestSoundGetText(ItemProperty itemProp, Vector3 position) {

        int randValue;
        EventReference reference;
        string returnValue;

        switch (itemProp) {
            case ItemProperty.CoffeeCup:
                randValue = UnityEngine.Random.Range(0, coffeeTexts.Length);
                reference = coffeeEvent;
                returnValue = coffeeTexts[randValue];
                break;
            case ItemProperty.PaperStack:
                randValue = UnityEngine.Random.Range(0, printerTexts.Length);
                reference = printerEvent;
                returnValue = printerTexts[randValue];
                break;
            case ItemProperty.Mail:
                randValue = UnityEngine.Random.Range(0, postTexts.Length);
                reference = postEvent;
                returnValue = postTexts[randValue];
                break;
            default:
                throw new NotImplementedException();
        }

        PlayOneShot(reference, position, randValue);
        return returnValue;
    }
}
