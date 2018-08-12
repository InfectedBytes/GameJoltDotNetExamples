using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter.Core {
	internal sealed class World {
		private readonly List<Entity> newEntities = new List<Entity>();
		private readonly List<Entity> entities = new List<Entity>();

		public IReadOnlyCollection<Entity> Entities => entities;

		public void Add(Entity entity) {
			entity.World = this;
			newEntities.Add(entity);
		}

		public void Update(GameTime gameTime) {
			entities.AddRange(newEntities);
			newEntities.Clear();
			// complexity of O(n²), 
			// therefore this should not be used for large projects with tons of entities
			for(int i = 0; i < entities.Count - 1; i++) {
				var entity = entities[i];
				for(int j = i + 1; j < entities.Count; j++) {
					var other = entities[j];
					if(entity.Team != other.Team) {
						entity.OnCollide(other);
						other.OnCollide(entity);
					}
				}
			}
			// update all entities that are still alive
			foreach(var entity in entities.Where(e => !e.IsDestroyed)) {
				entity.Update(gameTime);
			}
			// remove destroyed entities
			entities.RemoveAll(e => e.IsDestroyed);
		}

		public void Draw(SpriteBatch spriteBatch) {
			foreach(var entity in entities) {
				entity.Draw(spriteBatch);
			}
		}
	}
}
