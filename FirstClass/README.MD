CLASS 1


This manual page is a great introduction for how to navigate the scene in 3d. 

https://docs.unity3d.com/Manual/SceneViewNavigation.html


If you have never used a 3d design program this can be a bit of a challenge to get used to, but once you work with it a bit it starts to make sense. My best advice is to be constantly rotating the camera as you work. If you try to work from just one direction, often the depth will be off. It also helps to use the axis arrows instead of the free move, since it's hard to move in more than one axis on a flat display. I've seen a few neat things with using VR and 3d positioning, but it's just not practical at this point. 


Step one of our project is to make a scene. Easy to do. File -> New Scene
A scene is the location where everything happens in unity. Every object, every menu, every level will be a scene. 


Next step is to actually start laying out our game level. First thing we want to do is make the ground. We can do this by creating a quad (Gameobject -> 3D object -> Quad) now we want to move it to the center of the scene. Position 0,0,0. Using the inspector, under the transform component. You also want to rotate it 90 Degrees in the x, so it's flat like the ground should be. If you can't see the quad, you may be looking at it from behind. Quads only have 1 side, and nothing is rendered the other way. 


Next we can create a sphere. Gameobject -> 3D object -> Sphere. 
This sphere will also go at world 0,0,0. However you will notice its partially in the ground. You will need to lift the sphere up out of the ground a bit, so it doesn't fall through. The next thing we need is to add a rigidbody to the sphere. With the sphere selected, click on Component -> Physics -> Rigidbody. The Rigidbody is the component that handle actually moving and applying physics to the object. If you run the game now you will see the ball fall onto the ground. 


You might notice that the ball and the ground are the same colour. That's not really fun, but it's easy to fix. For simple organization you want to add a new folder in the project, and inside that folder create a material (Either through the create menu, or by right clicking in the project window). Create -> Material. Once the material has been created, you can change the color by changing the value in the “Albedo” field. Make 2 materials of whatever color you like, and you can apply them to the objects either by dragging them onto the object in the scene view, or into the hierarchy. 


You might also notice we don't have a camera yet. Create one by clicking GameObject -> Camera. You can move the camera around and see what it sees either in the game view tab, or the camera preview that should show up in the lower right corner when you have the camera selected.


Next step is creating scripts. Create a new folder in the project for the scripts. Create a new C# script and call it Player. 
This is the version at the end of the first class


This script handles player movement. 
It has 3 public fields, its own rigidbody, a float for its move speed, and an int for score.
In Update, we get input from Input.GetAxis(“Horizontal”) and Input.GetAxis(“Vertical”) 


We then multiply it by moveSpeed and call Rigidbody.AddForce with our new value.


In OnCollisionEnter we Check to see if we collide with an object with the tag pickup and if we do destroy it and increment our score.  (This will be covered later, and was added after the pickups were made)


For moving our camera with our character, we first made the camera a child of the ball object. This works when we move the ball around in the scene, but once the player is controlling the ball we see that the camera now rotates with the ball. This obviously doesn't work for what we need. 


Our solution was to create a new script, called Camera.cs
https://raw.githubusercontent.com/ProtoJazz/GameDevClass/master/FirstClass/Assets/Scripts/Camera.cs


This script has a reference to our player (Whatever object we want the camera to follow) and a Vector3 offset that is how far away we want the camera to be in all 3 directions. This does not control the camera's rotation, so you can just rotate it to the desired position. 


To keep our player in, we made a few simple walls out of cubes. Create a new GameObject->3d Object -> Cube


Press r, or select the scale tool to stretch the cubes to cover the sides of our level. This might be a good time to make the level bigger if desired. Just increase the scale of the ground quad to about 25, 25, 25 or so. Then make the cubes line the edges to keep the ball in. 


The next thing we did was create a cube that could be collected. Again we just made a cube from the gameobject menu, but this time we left it at its default size, and placed it somewhere away from the ball. Add a rigid body and make a new script. 
The script ended up like this : 
https://raw.githubusercontent.com/ProtoJazz/GameDevClass/master/FirstClass/Assets/Scripts/Pickup.cs


Right now the script doesn't actually do anything. But it will in the next class. 
To make them actually able to be picked up we add the Pickup tag to our pickup object. 
https://docs.unity3d.com/Manual/Tags.html


Once this has the correct tag, the OnCollisionEnter event we added to the player class will check for tags, if they match Pickup, it will call the code to remove the pickup, and increment the score. 


We ended the class by adding a simple UI, by clicking Create -> UI -> Text. The UI doesn't actually function yet, but will be the first thing completed next class. 
