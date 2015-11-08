﻿using UnityEngine;
using System.Collections;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;
using System.Collections.Generic;



public class Box2dTest : MonoBehaviour {

	/*
	// 2. create a ground body  
	// a. body definition  
	// b2BodyDef groundBodyDef;  
	groundBodyDef.position.Set(0.0f, -10.0f);  
	// b. creaet body  
	b2Body* groundBody = world.CreateBody(&groundBodyDef);  
	// c. create shape  
	b2PolygonShape groundBox;  
	groundBox.SetAsBox(50.0f, 10.0f);   // size of box is 100m*20m  
	// d. create fixture  
	groundBody->CreateFixture(&groundBox, 0.0f);    // 0.0f is density, for static body, mass is zero, so density is not used.  
	 */
	List<Body> bodyList = new List<Body>();
	World world;
	// Use this for initialization
	void Start () {
		
		AABB aabb = new AABB();
		aabb.UpperBound = new UnityEngine.Vector2(1000,1000);
		aabb.LowerBound = new UnityEngine.Vector2(-1000,-1000);
		world = new World(aabb, new Vector2(0, -10), false);

		CreateBox (0, 0, 10, 10,true);
		CreateBox (0, 5, 1, 1,false);

	}

	void CreateBox(float x,float y,float hwidth,float hheight,bool isStatic){
		BodyDef groundBodyDef = new BodyDef ();
		groundBodyDef.Position.Set (x, y);
		
		Body body = world.CreateBody (groundBodyDef);
		bodyList.Add (body);
		PolygonDef groundBox = new PolygonDef ();
		groundBox.SetAsBox (hwidth, hheight);
		Fixture fixture = body.CreateFixture (groundBox);

		if (isStatic == false) {
			fixture.Density = 1;
			fixture.Friction = 0.3f;

			body.SetMassFromShapes ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		world.Step (1f / 60f, 6, 2);
	}

	void OnDrawGizmos(){
		foreach (Body body in bodyList) {
			Fixture fixture = body.GetFixtureList();
			if(fixture.ShapeType== ShapeType.CircleShape)
			{

			}
			else if (fixture.ShapeType== ShapeType.PolygonShape){
				PolygonShape ps = fixture.Shape as PolygonShape;
				if(ps.Vertices.Length>=4)
				for (int i = 0; i < 4; i++) {
					int idx1 = i;
					int idx2 = i+1;
					if(idx2>=4)
					{
						idx2 = 0;
					}
					Vector2 pos = body.GetPosition();
					Gizmos.DrawLine(pos+ ps.Vertices[idx1],pos+ps.Vertices[idx2]);
				}
			}
		}
	}
}










