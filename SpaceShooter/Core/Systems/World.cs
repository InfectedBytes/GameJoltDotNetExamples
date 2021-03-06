﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooter.Core.Entities;
using SpaceShooter.Core.Events;

namespace SpaceShooter.Core.Systems {
	/// <summary>
	/// Manages all entities in the game.
	/// </summary>
	internal sealed class World : BaseSystem {
		private readonly List<Entity> newEntities = new List<Entity>();
		private readonly List<Entity> entities = new List<Entity>();

		public IReadOnlyCollection<Entity> Entities => entities;

		public World() {
			EventBroker.Register<SpawnEvent>(Spawn);
		}
		
		private void Spawn(SpawnEvent e) {
			// a SpawnEvent was dispatched by the EventBroker
			// we do not add the new entity immediately, but instead at the start of the next frame.
			newEntities.Add(e.Entity);
		}

		public override void Update(GameTime gameTime) {
			// add pending entities
			entities.AddRange(newEntities);
			newEntities.Clear();
			// complexity of O(n²), 
			// therefore this should not be used for large projects with tons of entities
			for(int i = 0; i < entities.Count - 1; i++) {
				var entity = entities[i];
				for(int j = i + 1; j < entities.Count; j++) {
					var other = entities[j];
					if(entity.Team != other.Team && entity.Overlaps(other)) {
						entity.OnCollide(other);
						other.OnCollide(entity);
					}
					if(entity.IsDestroyed) break;
				}
			}
			// update all entities that are still alive
			foreach(var entity in entities.Where(e => !e.IsDestroyed)) {
				entity.Update(gameTime);
			}
			// remove destroyed entities
			entities.RemoveAll(e => e.IsDestroyed || e.IsOutOfBounds());
		}

		public override void Draw(SpriteBatch spriteBatch) {
			foreach(var entity in entities) {
				entity.Draw(spriteBatch);
			}
		}
	}
}
