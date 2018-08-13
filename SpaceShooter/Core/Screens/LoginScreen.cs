﻿using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;
using SpaceShooter.Utils;
using SpaceShooter.Utils.External.MonoGameTextbox;

namespace SpaceShooter.Core.Screens {
	internal sealed class LoginScreen : Screen {
		private readonly TextureRegion2D cursor = Assets.Sprites["spaceMissiles_001"];
		private readonly LabeledTextbox username, token;
		private readonly SimpleButton loginButton;
		private readonly Label message;
		private bool requestInProgress;

		public LoginScreen() {
			KeyboardInput.Initialize(Game.Instance, 500f, 20);
			
			var size = new Point(300, 40);
			username = new LabeledTextbox(Assets.FontSmall, "Username:", new Point(0, -50), size);
			token = new LabeledTextbox(Assets.FontSmall, "Token:", new Point(0, 0), size);
			loginButton = new SimpleButton(Assets.FontSmall, "Login", new Point(0, 50), new Point(200, 40), OnClick);
			message = new Label("", new Vector2(0, 100));
			username.Active = true;
		}

		private void OnClick() {
			if(string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(token.Text)) {
				message.Text = "Username/token must not be empty";
				return;
			}
			requestInProgress = true;
			message.Text = "Logging in ...";
			Game.Jolt.Users.Auth(username.Text, token.Text, response => {
				requestInProgress = false;
				if(!response.Success) {
					message.Text = response.Message;
				} else {
					message.Text = null;
					Game.User = response.Data;
					Game.Instance.PushScreen(new MenuScreen());
				}
			});
		}

		public override void Unload() {
			base.Unload();
			username.Dispose();
			token.Dispose();
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			if(requestInProgress) return;
			if(Input.IsLeftMouseJustReleased()) {
				if(username.Contains(Input.MousePos)) {
					username.Active = true;
					token.Active = false;
				} else if(token.Contains(Input.MousePos)) {
					username.Active = false;
					token.Active = true;
				}
			}
			KeyboardInput.Update();
			username.Update();
			token.Update();
		}

		public override void Draw(GameTime gameTime) {
			base.Draw(gameTime);
			SpriteBatch.Begin(transformMatrix: Camera.Transform);
			username.Draw(SpriteBatch);
			token.Draw(SpriteBatch);
			loginButton.Draw(SpriteBatch);
			message.Draw(SpriteBatch);
			SpriteBatch.Draw(cursor, Input.MousePos, MathHelper.ToRadians(-30));
			SpriteBatch.End();
		}
	}
}
