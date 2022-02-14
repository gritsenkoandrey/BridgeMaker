﻿using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Managers
{
    public sealed class Manager : IInitializable, IDisposable
    {
        private readonly MCamera _camera;
        private readonly MConfig _config;
        private readonly MGame _game;
        private readonly MGUI _gui;
        private readonly MInput _input;
        private readonly MLight _light;
        private readonly MWorld _world;

        private static readonly Dictionary<Type, BaseManager> Container = new Dictionary<Type, BaseManager>();

        public Manager(
            MCamera camera, 
            MConfig config, 
            MGame game, 
            MGUI gui, 
            MInput input, 
            MLight light, 
            MWorld world)
        {
            _camera = camera;
            _config = config;
            _game = game;
            _gui = gui;
            _input = input;
            _light = light;
            _world = world;
        }

        public static T Resolve<T>() where T : BaseManager
        {
            if (!Container.ContainsKey(typeof(T)))
            {
                return null;
            }
            
            return Container[typeof(T)] as T;
        }

        public void Initialize()
        {
            Container.Add(typeof(MCamera), _camera);
            Container.Add(typeof(MConfig), _config);
            Container.Add(typeof(MGame), _game);
            Container.Add(typeof(MGUI), _gui);
            Container.Add(typeof(MInput), _input);
            Container.Add(typeof(MLight), _light);
            Container.Add(typeof(MWorld), _world);
        }

        public void Dispose()
        {
            Container.Clear();
            
            _camera.Dispose();
            _config.Dispose();
            _game.Dispose();
            _gui.Dispose();
            _input.Dispose();
            _light.Dispose();
            _world.Dispose();
        }
    }
}