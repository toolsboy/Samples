﻿// Copyright (C) 2012-2013 Weekend Game Studio
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveEngine.Framework;
using WaveEngine.Components;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Framework.Services;
using ScreenLayersProject;
using WaveEngine.Framework.Graphics;
using WaveEngine.Materials;
using WaveEngine.Components.Cameras;
using WaveEngine.Framework.UI;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.UI;
using WaveEngine.Framework.Physics2D;

namespace ScreenLayersProject
{
    public class MainScene : Scene
    {
        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.Black;

            var startButtonEntity = new Entity("StartButton")
            .AddComponent(new Transform2D())
            .AddComponent(new TextControl("Content/SegoeBlack20.wpk")
            {
                Text = "Press here",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Color.White,
            })
            .AddComponent(new TextControlRenderer())
            .AddComponent(new RectangleCollider())
            .AddComponent( new TouchGestures());

            startButtonEntity.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(MyScene_TouchPressed);

            EntityManager.Add(startButtonEntity);

            var screenLayerStateEntity = new Entity("ScreenLayerStateButton")
            .AddComponent(new Transform2D())
            .AddComponent(new TextControl("Content/SegoeBlack20.wpk")
            {
                Text = string.Format("Screen state: {0}", WaveServices.ScreenLayers.Tag),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Foreground = Color.White,
            })
            .AddComponent(new TextControlRenderer());

            EntityManager.Add(screenLayerStateEntity);
        }

        void MyScene_TouchPressed(object sender, GestureEventArgs e)
        {
            WaveServices.ScreenLayers.AddScene<FirstScene>()
                                     .AddScene<SecondScene>()
                                     .Apply("Menu");
        }

        protected override void Start()
        {
            base.Start();

            EntityManager.Find("ScreenLayerStateButton").FindComponent<TextControl>().Text = string.Format("Screen state: {0}", WaveServices.ScreenLayers.Tag);
        }
    }
}