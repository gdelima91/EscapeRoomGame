using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCodeDictionary : MonoBehaviour
{
    public string message = "Babe";
    public float dotSpeed = 0.25f; // speed of a dot code
    public float lineSpeed = 0.75f; // speed of a line code
    public float endSpace = 0.25f; // space after each letter
    public float letterSpace = 0.5f; // space between letters
    public float wordSpace = 0.5f; // space between each word

    public GameObject SignalLight;

    public Dictionary<char, float[]> morseCode = new Dictionary< char, float[] >();

    void Start( )
    {
        SignalLight.SetActive( false );

        morseCode.Add( 'a', new float[] { dotSpeed, lineSpeed } );
        morseCode.Add( 'b', new float[] { lineSpeed, dotSpeed, dotSpeed, dotSpeed } );
        morseCode.Add( 'c', new float[] { lineSpeed, dotSpeed, lineSpeed, dotSpeed } );
        morseCode.Add( 'd', new float[] { lineSpeed, dotSpeed, dotSpeed } );
        morseCode.Add( 'e', new float[] { dotSpeed } );
        morseCode.Add( 'f', new float[] { dotSpeed, dotSpeed, lineSpeed, dotSpeed } );
        morseCode.Add( 'g', new float[] { lineSpeed, lineSpeed, dotSpeed } );
        morseCode.Add( 'h', new float[] { dotSpeed, dotSpeed, dotSpeed, dotSpeed } );
        morseCode.Add( 'i', new float[] { dotSpeed, dotSpeed } );
        morseCode.Add( 'j', new float[] { dotSpeed, lineSpeed, lineSpeed, lineSpeed } );
        morseCode.Add( 'k', new float[] { lineSpeed, dotSpeed, lineSpeed } );
        morseCode.Add( 'l', new float[] { dotSpeed, lineSpeed, dotSpeed, dotSpeed } );
        morseCode.Add( 'm', new float[] { lineSpeed, lineSpeed } );
        morseCode.Add( 'n', new float[] { lineSpeed, dotSpeed } );
        morseCode.Add( 'o', new float[] { lineSpeed, lineSpeed, lineSpeed } );
        morseCode.Add( 'p', new float[] { dotSpeed, lineSpeed, lineSpeed, dotSpeed } );
        morseCode.Add( 'q', new float[] { lineSpeed, lineSpeed, dotSpeed, lineSpeed} );
        morseCode.Add( 'r', new float[] { dotSpeed, lineSpeed, dotSpeed } );
        morseCode.Add( 's', new float[] { dotSpeed, dotSpeed, dotSpeed } );
        morseCode.Add( 't', new float[] { lineSpeed } );
        morseCode.Add( 'u', new float[] { dotSpeed, dotSpeed, lineSpeed } );
        morseCode.Add( 'v', new float[] { dotSpeed, dotSpeed, dotSpeed, lineSpeed } );
        morseCode.Add( 'w', new float[] { dotSpeed, lineSpeed, lineSpeed } );
        morseCode.Add( 'x', new float[] { lineSpeed, dotSpeed, dotSpeed, lineSpeed } );
        morseCode.Add( 'y', new float[] { lineSpeed, dotSpeed, lineSpeed, lineSpeed } );
        morseCode.Add( 'z', new float[] { lineSpeed, lineSpeed, dotSpeed, dotSpeed } );

        StartCoroutine( StartCode() );
    }

    IEnumerator StartCode( )
    {
        string temp = message.ToLower();
        int step = 0;

        while( step <= temp.Length - 1 )
        {
            if( morseCode.ContainsKey( temp[step] ) )
            {
                int speed = 0;
                while( speed < morseCode[temp[step]].Length ) // goes through the float[]
                {
                    float wait = morseCode[temp[step]][speed];
                    SignalLight.SetActive( true );
                    //Debug.Log( "Letter : " + temp[step] + ", Wait Time : " + wait );
                    yield return new WaitForSeconds( wait );
                    SignalLight.SetActive( false );
                    yield return new WaitForSeconds( endSpace );
                    speed++;
                }
                yield return new WaitForSeconds( letterSpace );
            }
            else
            {
                yield return new WaitForSeconds( wordSpace );
            }
            step++;
        }
        //Debug.Log( "Finished Code" );
        StartCoroutine( StartCode() );
    }
}
