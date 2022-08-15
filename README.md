Primitive War by Loïc Bouvier

Project description

When it comes to code architecture, I followed the architecture presented in the "Unite Austin 2017 - Game Artchitecture with Scriptable Objects" talk 
coupled to "SOLID principles" (especially the single-responsibility principle and the open-closed principle).
It allows me to create maintenable and modular code that can easily be understood by other parties.

All data not generated at runtime are stored in Scriptable Object data containers. 
It reduces serialization needs and allow to modify datas in one place only.

Communication between scripts happens either using Unit Events or Scriptable Object events. 
Most of the time, this allow me to avoid referencing other monobehavior directly to make my code more modular. 
Each component serves one responisibility, manage its own dependencies and is made as reusable as possible.

I avoid using Singleton to prevent issues linked to this pattern when working on bigger project.

I try to write code that don't require lots of commenting but instead rely on clear separation of code and correctly named self documenting functions and variables.

On a bigger project I would separate Manager, Gameplay recurrent elements (UI, TimeManager, etc...) and level elements in different scenes
that are loaded additively and use the Adressable system to manage my content more efficiently.

Performance optimization:
- I used object pooling for effects.
- I compared the sqrDistance of units in the targetSetter script to avoid calculating a square root.
- I didn't update the target everyframe as it doesn't have any impact on gameplay to calculate it less often.
- (With extra time, I would use pooling on units army also in order to limit Garbage collection when playing with armies settings. It would require to update
 the customization system to save units configuration and apply it when pooling a unit.)

I built the army generating system in such a way that it allow adding extra armies out of the box. 
Just create another Army Scriptable Object and assign it to the armies scriptable Object array. 
The Unit manager will generate as many armies as referenced in the array. Each with their own customizable settings.

For the additional feature, I added the ability to customize the army size and disposition before battle. It lets player test various starting
placement strategy and provides more playability with this simple concept. Player can also randomize all armies by clicking a button or customize a specific
unit by clicking on it directly.

With more time, I would push further the idea of army customization. Adding different formation, allowing to control unit customization more precisely 
(like changing only the shape, color or size of each unit).

--------------------------------------------------------------------------------------------

Plugins required:

Odin Inspector:
https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041

LeanPool:
https://assetstore.unity.com/packages/tools/utilities/lean-pool-35666

DoTween:
http://dotween.demigiant.com/