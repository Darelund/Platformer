using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    public class GameObjectFactory
    {
        public static GameObject CreateGameObject((string type, Rectangle rec) data)
        {
            var type = data.type;
            return type switch
            {
                "enemies" => CreateEnemyController(data.rec),
                "player" => CreatePlayerController(data.rec),
                "platforms" => CreatePlatform(data.rec),
                "item" => CreatePlayerController(data.rec),
                _ => CreatePlatform(data.rec)
                //Will maybe add back
                //case 'T':
                //    return CreateTeleport(data.Position);
            };
        }

        private static EnemyController CreateEnemyController(Rectangle data)
        {
            string sprite = "ghost";
            //Offset because character is bigger than tiles
            Vector2 positionOffset = new Vector2(16, 16);
            Rectangle rect = data;

            //float speed = float.Parse(data[2]);
            Color color = Color.White;
           
            float rotation = 0;
            float size = 2f;
            float layerDepth = 0.2f;


            Vector2 origin = new Vector2(8f, 8f);

            Random randomAnimation = new Random();
            int animationToPick = randomAnimation.Next(1, 5);

            int yAnimationOffset = 16 * animationToPick;
            int xAnimationOffset = 16;

            Rectangle[] enemyRoaming =
             {
                new Rectangle(xAnimationOffset * 0, yAnimationOffset , 16, 16),
                new Rectangle(xAnimationOffset * 1, yAnimationOffset, 16, 16),
                new Rectangle(xAnimationOffset * 2, yAnimationOffset, 16, 16),
                new Rectangle(xAnimationOffset * 3, yAnimationOffset, 16, 16),
                new Rectangle(xAnimationOffset * 4, yAnimationOffset, 16, 16),
                new Rectangle(xAnimationOffset * 5, yAnimationOffset, 16, 16),
                new Rectangle(xAnimationOffset * 6, yAnimationOffset, 16, 16),
                new Rectangle(xAnimationOffset * 7, yAnimationOffset, 16, 16)
            };
            //Rectangle[] enemyChasing =
            // {
            //    new Rectangle(0, 0, 39, 39),
            //    new Rectangle(40, 0, 39, 39),
            //    new Rectangle(80, 0, 39, 39),
            //    new Rectangle(120, 0, 39, 39)
            //};
            Rectangle[] enemyFleeingStage1 =
             {
                new Rectangle(xAnimationOffset * 0, 16 * 5, 16, 16),
                new Rectangle(xAnimationOffset * 1, 16 * 5, 16, 16),
            };
            Rectangle[] enemyFleeingStage2 =
            {
                new Rectangle(xAnimationOffset * 2, 16 * 5, 16, 16),
                new Rectangle(xAnimationOffset * 3, 16 * 5, 16, 16),
            };
            Rectangle[] enemyDead =
             {
                new Rectangle(xAnimationOffset * 4, 16 * 5, 16, 16),
                new Rectangle(xAnimationOffset * 5, 16 * 5, 16, 16),
                new Rectangle(xAnimationOffset * 6, 16 * 5, 16, 16),
                new Rectangle(xAnimationOffset * 7, 16 * 5, 16, 16),
            };

            Dictionary<string, AnimationClip> animationClips = new Dictionary<string, AnimationClip>()
            {
                {"Roaming",  new AnimationClip(enemyRoaming, 3f)},
               // {"Chasing",  new AnimationClip(enemyRoaming, 7f)},
                {"Fleeing1",  new AnimationClip(enemyFleeingStage1, 10f)},
                {"Fleeing2",  new AnimationClip(enemyFleeingStage2, 10f)},
                {"Dead",  new AnimationClip(enemyDead, 7f)},
            };

            return new EnemyController(
                ResourceManager.GetTexture(sprite),
                rect,
                color,
                rotation,
                layerDepth,
                origin,
                animationClips
            );
        }
        private static PlayerController CreatePlayerController(Rectangle data)
        {
            string sprite = "mario-pauline-transparent";
            //Offset because character is bigger than tiles
            Vector2 positionOffset = new Vector2(14.5f, 14.5f);
            Rectangle rect = data;

            //float speed = float.Parse(data[2]);
            string colorName = GameFiles.Character.CHARACTERCOLOR;
            Color color = colorName switch
            {
                "white" => Color.White,
                "yellow" => Color.Yellow,
                "red" => Color.Red,
                "blue" => Color.Blue,
                "green" => Color.Green,
                "pink" => Color.Purple,
                _ => Color.White
            };
            float rotation = 0;
            float layerDepth = 0;

            
            Vector2 origin = new Vector2(19.5f, 19.5f);

            Rectangle[] idle =
           {
                new Rectangle(16, 1, 16, 16),
            };

            Rectangle[] walk =
             {
                new Rectangle(1, 1, 16, 16),
                new Rectangle(18, 1, 16, 16),
                new Rectangle(35, 1, 16, 16),
            };
            Rectangle[] sprint =
            {
                new Rectangle(1, 1, 16, 16),
                new Rectangle(18, 1, 16, 16),
                new Rectangle(35, 1, 16, 16),
            };

            Rectangle[] attack =
            {
                new Rectangle(52, 1, 16, 16),
                new Rectangle(69, 1, 16, 16),
                new Rectangle(86, 1, 16, 16),
                new Rectangle(103, 1, 16, 16),
                new Rectangle(120, 1, 16, 16),
                new Rectangle(137, 1, 16, 16),
            };

            Rectangle[] die =
            {
                new Rectangle(256, 1, 16, 16),
                new Rectangle(273, 1, 16, 16),
                new Rectangle(290, 1, 16, 16),
                new Rectangle(307, 1, 16, 16),
                new Rectangle(324, 1, 16, 16),
            };
            Rectangle[] climb =
            {
                new Rectangle(154, 1, 16, 16),
                new Rectangle(154, 1, 16, 16),
                new Rectangle(154, 1, 16, 16),
            };

            Dictionary<string, AnimationClip> animationClips = new Dictionary<string, AnimationClip>()
            {
                {"Idle",  new AnimationClip(idle, 7f)},
                {"Walk",  new AnimationClip(walk, 7f)},
                {"Sprint",  new AnimationClip(sprint, 10f)},
                {"Attack",  new AnimationClip(attack, 7f)},
                {"Die",  new AnimationClip(die, 4f)},
                {"Climb",  new AnimationClip(climb, 6f)}
            };

            return new PlayerController(
                ResourceManager.GetTexture(sprite),
                rect,
                color,
                rotation,
                layerDepth,
                origin,
                animationClips
            );
        }

        //private static Item CreateItem(Rectangle data)
        //{
        //    Random ran = new Random();

        //    //Size (13, 14)
        //    string sprite = "ghost";
            
        //    Rectangle[] itemRect =
        //     {
        //        new Rectangle(0, 0, 15, 15),
        //        new Rectangle(1, 97, 13, 14),
        //        new Rectangle(17, 97, 13, 14),
        //        new Rectangle(32, 97, 13, 14),
        //        new Rectangle(48, 97, 13, 14),
        //        new Rectangle(65, 97, 13, 14),
        //        new Rectangle(81, 97, 13, 14),
        //        new Rectangle(97, 97, 13, 14),
        //        new Rectangle(113, 97, 13, 14),
        //    };
        //    Rectangle ranRect = itemRect[ran.Next(0, itemRect.Length)];

        //    Vector2 positionOffset = new Vector2(11f, 11f);

        //    Rectangle rect = data;

        //    Color color = Color.White;

        //    float rotation = 0f;
        //    float size = 1.5f;
        //    float layerDepth = 0.2f;

        //    Vector2 origin = new Vector2(6.5f, 7f);

        //    int ranItem = ran.Next(0, 101);

        //    if(ranItem >= 0 && ranItem < 75)
        //    {
        //        return new Candy(
        //            ResourceManager.GetTexture(sprite),
        //            position,
        //            ranRect,
        //            color,
        //            rotation,
        //            size,
        //            layerDepth,
        //            origin
        //        );
        //    }
        //    else if (ranItem >= 75 && ranItem < 85)
        //    {
        //        return new Consumble(
        //           ResourceManager.GetTexture(sprite),
        //           rect,
        //           ranRect,
        //           color,
        //           rotation,
        //           layerDepth,
        //           origin
        //       );
        //    }
        //    else if (ranItem >= 85 && ranItem < 90)
        //    {
        //        return new Wearable(
        //            ResourceManager.GetTexture(sprite),
        //            rect,
        //            ranRect,
        //            color,
        //            rotation,
        //            layerDepth,
        //            origin
        //        );
        //    }
        //    else
        //    {
        //        return new Weapon(
        //              ResourceManager.GetTexture(sprite),
        //              rect,
        //              ranRect,
        //              color,
        //              rotation,
        //              layerDepth,
        //              origin
        //          );
        //    }
        //}
        private static Platform CreatePlatform(Rectangle data)
        {
            Rectangle rect = data;
            string sprite = "wall";
            //Vector2 positionOffset = new Vector2(14.5f, 14.5f);
            PlatformType type = PlatformType.Unbreakable;
            //float speed = float.Parse(data[2]);
            Color color = Color.White;
            //Color color = colorName switch
            //{
            //    "white" => Color.White,
            //    "yellow" => Color.Yellow,
            //    "red" => Color.Red,
            //    "blue" => Color.Blue,
            //    "green" => Color.Green,
            //    "pink" => Color.HotPink,
            //    _ => Color.White
            //};

            float rotation = 0;
            float size = 1f;
            float layerDepth = 0.9f;


            return new Platform(
                ResourceManager.GetTexture(sprite),
                rect,
                type,
                color,
                rotation,
                layerDepth
                );
        }
        //private Teleport CreateTeleport(Vector2 data)
        //{
        //    string sprite = "empty";
        //    //Vector2 positionOffset = new Vector2(14.5f, 14.5f);
        //    Vector2 position = data;

        //    //float speed = float.Parse(data[2]);
        //    Color color = Color.White;
        //    //Color color = colorName switch
        //    //{
        //    //    "white" => Color.White,
        //    //    "yellow" => Color.Yellow,
        //    //    "red" => Color.Red,
        //    //    "blue" => Color.Blue,
        //    //    "green" => Color.Green,
        //    //    "pink" => Color.HotPink,
        //    //    _ => Color.White
        //    //};
        //    float rotation = 0;
        //    float size = 0.8f; // To make it same size as the other tiles
        //    float layerDepth = 0.1f;


        //    Vector2 origin = new Vector2(0, 0);

           

        //    return new Teleport(
        //        ResourceManager.GetTexture(sprite),
        //        position,
        //        color,
        //        rotation,
        //        size,
        //        layerDepth,
        //        origin
        //    );
        //}
    }
}
