#Outline for class 2

First we kept going with updating and getting our UI to work. 

> We made lots of changes to our Player script in the process 

https://github.com/ProtoJazz/GameDevClass/blob/master/SecondClass/Assets/Scripts/Player.cs

Steps taken for this part were :
* Added the using statement at line 3

`using UnityEngine.UI;`

* Added the public reference at line 11

`public Text scoreText;`

* Then at line 61 we set the score

`scoreText.text = "Score " + this.score;`

> Note these line numbers are based on the finished file at the end of the second class, not the beginning

These changes allowed our UI to actually keep score, and update to whatever value our score was, as well as whatever text we wanted
> Note that any text you entered in line 61 `scoreText.text = "TEXT " + this.score` is what the ui would say, including spaces and 
puntuation. So in the original you would have "Score 5" show up on the ui, but in this one it would be "TEXT 5"

Usually Unity in single threaded. It doesnt matter too much what this means, but basically it means everything flows along one path. If we wanted to do a task that runs
along side our Update function, we need to make whats called a coroutine. It doesnt really matter how it works for now, but you can look 
up more information if you like. All we care about is this is how we can create a timer. 

First we created a new UI Text object (I just duplicated the curret score one) and we created a new public reference on our player script

* Line 12   `public Text timerText;`

The code for the coroutine is in the player script, line 33, the function called timer

This does a few basic things
* gets the current time 
> Unity keeps track of time as a number of seconds instead of an actual time (Like many computer systems) This makes calculating
our timer fairly each, since we just add the seconds together
* sets the ending time for our loop
* executes a while loop as long as we havent hit the time limit yet
* sets our UI text to the current time remaining (The difference between end and start times) 
* It also rounds the numbers from long floats such as `10.2426267` to whole numbers like `10` or `9`
* It then waits for the end of the current frame, and updates the time
* Once the time runs out, it leaves the loop and handles some code we setup for ending the game

#Ending the game

We setup some code that lets us end the game based on a few factors. Either we run out of time and the game ends, or we 
collect enough cubes and we win. 

First we setup the time over state
> A simple state machine could have been used here, but was left out for simplicity

* The first change we want to make is to see if the game is still runing, or if its ended
we set this on line 15 `public bool isGameRunning = true;`
We use it to keep the player from moving on line 24 by surrounding everything in our update function with an `if(isGameRunning)`
block. 

* When the timer is over, and the game is currently running we set isGameRunning to false (The game could stop running when we win too) 
We also display another UIText that says Game Over setup the same way as score text, except this time we toggle its active value
by calling SetActive(true). Any unity game object can have SetActive(true) or SetActive(false) called and set active or not. 
> Inactive objects will not have their updates run. If an object is inactive from the beginning, its start will also only run
once it is active

For the Win Condition, we made it based on the number of cubes you collect. 
* Added a property to player called scoreToWin (line 16). This is the number of cubes you must collect to reach the win
* In our collision section we added a check after we update the score text. Line 62 : ` if (score >= scoreToWin)`
* If true, we execute the code inside. Which sets isGameRunning to false, and activates a win text (Just like the game over text)


# Particle Effects

The next thing we did was add an explosion to picking up our cubes. For this we used a standard unity particle system, then upgraded 
to one we downloaded

First step is to create a prefab from our pickup cube. This is a very helpful system in Unity and allows us to make changes once
and have them propagate to all our intances of the same object. 
https://docs.unity3d.com/Manual/Prefabs.html

Once our Cube is a prefab, we can start to make changes to it. 
* First we created a script called Pickup 
https://github.com/ProtoJazz/GameDevClass/blob/master/SecondClass/Assets/Scripts/Pickup.cs

This script does the following 
* Has references to our pickup cubes prefab particle system (We can use any particle system, just save it as a prefab, and make sure you
use the prefab reference in the gameobject field instead of one from the scene. For the demo I just played with the settings on 
a default one until I liked it better, but later downloaded several that are in the project files.)
> Unitys video on particle systems https://unity3d.com/learn/tutorials/topics/graphics/particle-system

* We also have a reference to the cubes collider and renderer (These have to be attatched in the editor, and should be the cubes own 
collider and mesh renderer)
`   public GameObject particleSystem;
    public Collider ourCollider;
    public Renderer ourRenderer;`
* We then created a new function call Death after start (Line 12 of pickup)
`public void Death()`
* Inside there we do the following 
  * Instantiate the particle system    `GameObject particles = GameObject.Instantiate(particleSystem, transform.position, Quaternion.Euler(new Vector3(90,0,0))) as GameObject;`
  > This one is a little more complicated than other ones, what this does is creates a new copy of the particle
  system we added from our prefab, sets its position to match the cubes, then rotates it 90 degrees
  
  * Turns off our collider and renderer (We want it to look like it exploded, but we need it to clean up after its self once the particle is done animating
  * Then it queues up destruction of the particle system, and then its own object. Probably not needed, but keeps the scene clean 
  
  To actually call this we have to modify the player script again
  * In lines 58 and 59 of the player script we get the Pickup component from the object we collided with and call Death() on it
  > Note that if the object we hit didnt have a Pickup script on it, this would cause an error. However since we check the Tags on 
  our collisions on line 56, we shouldnt have any problems. As long as nothing that is not a pickup gets tagged Pickup
  
  After setting up the explosions we played around a bit with changing the model on the cubes.
  I used one of the flags from the 3D assets in the Kenny Asset pack mini golf folder.
  
  To make this even easier, all I did was attach the flag pole as a child of the cube, and uncheck the renderer of the cube its self
  and swapped the reference `ourRendeder` to the flagpole (Just drag the flag onto the ourRenderer field, it will sort it out)
  
  Then I just used the existing collider on the cube, but adjusted its size values and center values to match the pole better
  Once you turn off the renderer on the cube you should see a green box outline, this is the collider. Once you are happy 
  with its size and position. Click Apply at the top of the inspector to apply the changes to the prefab. And all the cubes should
  get the new values
  
  
  
