﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace ProjectStella
{
    /// <summary>
    /// Class for our lovely HiDef skybox.
    /// </summary>
    public class Skybox
    {
        #region Fields

        // The skybox cube model.
        private Model skyBox;

        // The texture for the above cube.
        private TextureCube skyBoxTexture;

        // The effect file that the skybox will use to render.
        private Effect skyBoxEffect;
        
        // The size of the cube that we can use to resize the box
        // for different sized environments.
        private float scale = 5000f;

        #endregion

        #region Initialize

        /// <summary>
        /// Constructor for the skybox.
        /// </summary>
        public Skybox(ContentManager content)
        {
            skyBox = content.Load<Model>("Skybox/cube");
            skyBoxTexture = content.Load<TextureCube>("Skybox/SkyBox"); ;
            skyBoxEffect = content.Load<Effect>("Skybox/SkyboxEffect");
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the skybox with the skybox effect.
        /// There is no world matrix because we're aassuming the skybox
        /// won't be moved around.
        /// </summary>
        /// <param name="view">View matrix for skybox effect</param>
        /// <param name="projection">Projection matrix for the effect</param>
        /// <param name="cameraPosition">Position of the camera.</param>
        public void Draw(Camera camera)
        {
            // Go through each pass in the effect.
            foreach (EffectPass pass in skyBoxEffect.CurrentTechnique.Passes)
            {
                // Draw all of the mesh components.
                foreach (ModelMesh mesh in skyBox.Meshes)
                {
                    // Assign the appropriate values to each of the parameters
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = skyBoxEffect;
                        part.Effect.Parameters["World"].SetValue(Matrix.CreateScale(scale) * Matrix.CreateTranslation(camera.Position));
                        part.Effect.Parameters["View"].SetValue(camera.ViewMatrix);
                        part.Effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);
                        part.Effect.Parameters["SkyBoxTexture"].SetValue(skyBoxTexture);
                        part.Effect.Parameters["CameraPosition"].SetValue(camera.Position);
                    }

                    // Draw the mesh with the skybox effect.
                    mesh.Draw();
                }
            }
        }

        #endregion
    }
}
