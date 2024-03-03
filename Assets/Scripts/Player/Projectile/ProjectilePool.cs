using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Utilities;
using ServiceLocator.Player;
/*  This script demonstrates the implementation of Object Pool design pattern.
 *  If you're interested in learning about Object Pooling, you can find
 *  a dedicated course on Outscal's website.
 *  Link: https://outscal.com/courses
 * */

namespace ServiceLocator.Player.Projectile
{
    public class ProjectilePool : GenericObjectPool<ProjectileController>
    {
        private PlayerService playerService;
        private ProjectileView projectilePrefab;
        private List<ProjectileScriptableObject> projectileScriptableObjects;
        private Transform projectileContainer;

        public ProjectilePool(ProjectileView projectilePrefab, List<ProjectileScriptableObject> projectileScriptableObjects,PlayerService playerService)
        {
            this.playerService = playerService;
            this.projectilePrefab = projectilePrefab;
            this.projectileScriptableObjects = projectileScriptableObjects;
            projectileContainer = new GameObject("Projectile Container").transform;
        }

        public ProjectileController GetProjectile(ProjectileType projectileType)
        {
            ProjectileController projectile = GetItem();
            ProjectileScriptableObject scriptableObjectToUse = projectileScriptableObjects.Find(so => so.Type == projectileType);
            projectile.Init(scriptableObjectToUse);
            return projectile;
        }

        protected override ProjectileController CreateItem() => new ProjectileController(projectilePrefab, projectileContainer,playerService);
    }
}