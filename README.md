# unity-space-invaders
Game made with Unity in a exam context.

## Game overview

This game is a replica of the famous Space Invaders game.
In this game, you control a spaceship able to blast lasers, nothing more.
It is quite convenient because the space is filled with other beings, and lasers are a good way to deal with 'em.
Be careful thought, some lads won't be happy with you firing at them and they will backfire !
The more ships you destroy, the more points you earn ( but think about all the lives you selfishly took you monster ! )

### Game mechanics
The gas pedal of your ship is broken, allowing your ship only to move right and left.
A press on the fire button will shoot a laser in front of the ship, destroying the first target on its way.
Enemies will come from both sides and bottom of the screen, and as they are social, always with friends, never alone.
The last enemy of the wave will drop a pill into space. Whether it contains a bonus for your ship or his last words about how he and his friends got murdered by a destruction-mad space gunner, it's better to collect it.

### UML

GameManager :
	Player : GameObject
	EnemyWave : GameObject
	lives : Integer to store the remainings lives of the player
	score : Integer to store the score of the player

Player :
	speed : Float
	laser : GameObject that the ship can Instantiate
	power : String which specify the power type the player currently has, none if no active power

	Fire() : Instanciate a laser 
	Move() : Move left or right according to the pressed key
	Power() : If the player has a power enabled, pressing the right key will use it.

Laser :


Enemy : Abstract class
	Fire() : Randomly fire a straight forward laser

	OnTriggerEnter2D() : Destroy and add points

EnemyBasic : Enemy
	sprite :
	points : 

EnemyShockWave : Enemy
	sprite :
	Fire() : Fire a circled laser larger than the others

EnemyBeam : Enemy
	sprite :
	Fire() : Fire a laser all the way to the top to block the player


EnemyWave :
	enemies: Array of Enemy
	dropX, dropY: Coordinates of the last enemy in the wave

	SelectWaveType() : Select the type of wave.
	SelectWaveComingSide : Select from where will the wave come.
	InstanciatePill() : Drop a pill when the last enemy dies
	Move() : Move the whole wave


Pill :
	speed : 
	type :
		Feu de dieu : Obliterate all opponents
		Lare


pourquoi : pour rendre les gens heureux
comment : en leur permettant de jouer facilement a des jeux de société
quoi : 
