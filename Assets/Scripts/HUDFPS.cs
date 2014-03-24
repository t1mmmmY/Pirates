using UnityEngine;
using System.Collections;

public class HUDFPS : MonoBehaviour 
{

// Attach this to a GUIText to make a frames/second indicator.
//
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
//
// It is also fairly accurate at very low FPS counts (<10).
// We do this not by simply counting frames per interval, but
// by accumulating FPS for each frame. This way we end up with
// correct overall FPS even if the interval renders something like
// 5.5 frames.
 
	public  float updateInterval = 0.5F;
 
	private float accum   = 0; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	
	string guiText = string.Empty;
	Color colorText = Color.black;
 
	void Start()
	{
    	timeleft = updateInterval;  
	}
 
	void Update()
	{
	    timeleft -= Time.deltaTime;
	    accum += Time.timeScale/Time.deltaTime;
	    ++frames;
	    
	    if( timeleft <= 0.0 )
	    {
	    	float fps = accum/frames;
	    	guiText = string.Format("{0:F2} FPS",fps);
	
	    	if(fps < 30 && fps >= 10)
			{
				colorText = Color.yellow;
			}
    		else if(fps < 10)
			{
				colorText = Color.red;
			}
        	else
			{
				colorText = Color.green;
			}
			
        	timeleft = updateInterval;
        	accum = 0.0F;
        	frames = 0;
    	}
	}
	
	void OnGUI()
	{
		GUI.color = colorText;
		GUI.skin.label.fontSize = 30;
		GUI.Label(new Rect(Screen.width - 100, 30, 100, 40), guiText);
	}
}