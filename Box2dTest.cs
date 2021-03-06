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
    class Box{
		const bool OPEN = false;
        public GameObject go;
        public Body body;
        public float width, height;
        public Box(Body body, float hwidth,float hheight) {
            this.body = body;
            this.width = hwidth*2;
            this.height = hheight*2;
			if(OPEN)
			{
	            go = BoxPool.instance.Get();
	            go.transform.localScale = new Vector3(width, height, 1);
			}
		}
        public void Update() {
			if (OPEN) {
				go.transform.position = body.GetPosition ();
				go.transform.eulerAngles = new Vector3 (0, 0, body.GetAngle ());
			}
        }

		public void Draw(){
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
	
	List<Box> boxList = new List<Box>();
	World world;
	Body testBody;
	
	// Use this for initialization
    void Start()
    {

		AABB aabb = new AABB ();
		aabb.UpperBound = new UnityEngine.Vector2 (1000, 1000);
		aabb.LowerBound = new UnityEngine.Vector2 (-1000, -1000);
		world = new World (aabb, new Vector2 (0, -10), false);

//		BodyDef bodyDef = new BodyDef ();
//		bodyDef.Position.Set (0, 0);
//
//		Body body = world.CreateBody (bodyDef);
//		PolygonDef pd = new PolygonDef ();
//		pd.SetAsBox (1, 1);
//		var fix = body.CreateFixture (pd);
//		fix.Density = 99999;
//
//		boxList.Add (new Box (body, 1, 1));
////		pd.Density = 10000;
//		body.SetMassFromShapes ();

		var body1 = CreateBox (0, 0, 10, 1, true);

		body1.GetFixtureList ().Friction = 1;

		var body2 = CreateBox (0, 3, 1, 1, false);
		body2.GetFixtureList ().Friction = 1;
		testBody = body1;
		//		testBody = CreateBox (0, 0, 10, 1, true);
//		CreateBox (0, 5, 1, 1, false);
//		PrismaticJointDef pd = new PrismaticJointDef ();
//		var body2 = CreateBox (0, 0, 1, 1, true);
//		pd.Body1 = testBody;
//		pd.Body2 = body2;
//
//		pd.CollideConnected = false;
//		pd.LowerTranslation = 0;
//		pd.UpperTranslation = 1;
//		pd.MaxMotorForce = 1;
	}

	Body CreateBox(float x,float y,float hwidth,float hheight,bool isStatic){
		BodyDef groundBodyDef = new BodyDef ();
		groundBodyDef.Position.Set (x, y);
        groundBodyDef.FixedRotation = true;
        
		Body body = world.CreateBody (groundBodyDef);
		PolygonDef groundBox = new PolygonDef ();
		groundBox.SetAsBox (hwidth, hheight);
		Fixture fixture = body.CreateFixture (groundBox);

//		if (isStatic == false) {
			fixture.Density = 1;
			fixture.Friction = 0.3f;

			body.SetMassFromShapes ();
  //      }
		if(isStatic)
		body.SetStatic ();
		Box box = new Box(body, hwidth, hheight);
        boxList.Add(box);
        return body;
	}
	
	// Update is called once per frame
	void Update () {
		world.Step (1f / 60f, 6, 2);
      //  testBody.SetPosition(new Vector2(testBody.GetPosition().x, 0));
        Draw();
	}

    void Draw() {
        foreach (var box in boxList)
        {
            box.Update();
        }
    }
    int idx = 0;
    void OnGUI() {
        if (GUILayout.RepeatButton("xxxxxxxxxxxx"))
        {
//			idx++;
//			CreateBox(0,idx*5,1,1,false);
			testBody.ApplyForce(Vector3.right*9999,Vector2.zero);
			//testBody.ApplyImpulse(Vector2.right*100,testBody.GetPosition());
			//SetPosition(testBody.GetPosition() + Vector2.right*Time.deltaTime);  //.ApplyImpulse(Vector2.right * 10, testBody.GetPosition());
        }
    }

	void OnDrawGizmos(){
		foreach (var box in boxList) {
			box.Draw();
		}
        //foreach (Body body in bodyList) {
        //    Fixture fixture = body.GetFixtureList();
        //    if(fixture.ShapeType== ShapeType.CircleShape)
        //    {

        //    }
        //    else if (fixture.ShapeType== ShapeType.PolygonShape){
        //        PolygonShape ps = fixture.Shape as PolygonShape;
        //        if(ps.Vertices.Length>=4)
        //        for (int i = 0; i < 4; i++) {
        //            int idx1 = i;
        //            int idx2 = i+1;
        //            if(idx2>=4)
        //            {
        //                idx2 = 0;
        //            }
        //            Vector2 pos = body.GetPosition();
        //            Gizmos.DrawLine(pos+ ps.Vertices[idx1],pos+ps.Vertices[idx2]);
        //        }
        //    }
        //}
	}
}










