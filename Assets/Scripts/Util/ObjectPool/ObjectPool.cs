using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool
{
    #region Fields

    private static int platformsAmount = 6;
    private static int objAmount = 2;
    private static int lavaAmount = 5;
    static Dictionary<PooledObjectName, List<GameObject>> pools;

    #endregion

    #region Initialize

    /// <summary>
    /// Initializes the pools
    /// </summary>
    public static void Initialize()
    {
        // initialize dictionary
        pools = new Dictionary<PooledObjectName, List<GameObject>>
        {
            {PooledObjectName.Platform, new List<GameObject>(platformsAmount)},
            {PooledObjectName.DnaXp, new List<GameObject>(objAmount)},
            {PooledObjectName.DnaHp, new List<GameObject>(objAmount)},
            {PooledObjectName.DnaInv, new List<GameObject>(objAmount)},
            
            {PooledObjectName.SphereDoubleJump, new List<GameObject>(objAmount)},
            {PooledObjectName.SphereTimeAcceleration, new List<GameObject>(objAmount)},
            {PooledObjectName.SphereTimeDilation, new List<GameObject>(objAmount)},
            
            {PooledObjectName.Lava, new List<GameObject>(lavaAmount)}
            
        };

        // fill Platform pool
        for (var i = 0; i < pools[PooledObjectName.Platform].Capacity; i++)
        {
            pools[PooledObjectName.Platform].Add(GetNewObject(PooledObjectName.Platform));
        }

        // fill Dna pool
        for (var i = 0; i < pools[PooledObjectName.DnaXp].Capacity; i++)
        {
            pools[PooledObjectName.DnaXp].Add(GetNewObject(PooledObjectName.DnaXp));
            pools[PooledObjectName.DnaHp].Add(GetNewObject(PooledObjectName.DnaHp));
            pools[PooledObjectName.DnaInv].Add(GetNewObject(PooledObjectName.DnaInv));
            pools[PooledObjectName.SphereDoubleJump].Add(GetNewObject(PooledObjectName.SphereDoubleJump));
            pools[PooledObjectName.SphereTimeAcceleration].Add(GetNewObject(PooledObjectName.SphereTimeAcceleration));
            pools[PooledObjectName.SphereTimeDilation].Add(GetNewObject(PooledObjectName.SphereTimeDilation));
        } 
        
        
        // fill Obs pool
        for (var i = 0; i < pools[PooledObjectName.Lava].Capacity; i++)
        {
            pools[PooledObjectName.Lava].Add(GetNewObject(PooledObjectName.Lava));
        }
    }

    #endregion

    #region Getters

    public static GameObject GetPlatform()
    {
        return GetPooledObject(PooledObjectName.Platform);
    }
    
    public static GameObject GetDnaXp()
    {
        return GetPooledObject(PooledObjectName.DnaXp);
    }
    public static GameObject GetDnaHp()
    {
        return GetPooledObject(PooledObjectName.DnaHp);
    } 
    public static GameObject GetDnaInv()
    {
        return GetPooledObject(PooledObjectName.DnaInv);
    }
    public static GameObject GetDJ()
    {
        return GetPooledObject(PooledObjectName.SphereDoubleJump);
    }
    public static GameObject GetTA()
    {
        return GetPooledObject(PooledObjectName.SphereTimeAcceleration);
    }
    public static GameObject GetTD()
    {
        return GetPooledObject(PooledObjectName.SphereTimeDilation);
    }
    public static GameObject GetLava()
    {
        return GetPooledObject(PooledObjectName.Lava);
    }
    
    public static GameObject GetPooledObject(PooledObjectName name)
    {
        var pool = pools[name];

        // check for available object in pool
        if (pool.Count > 0)
        {
            GameObject obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return obj;
        }

        // pool empty, so expand pool and return new object
        pool.Capacity++;
        if (name == PooledObjectName.Platform)
        {
            return GetNewObject(PooledObjectName.Platform);
        }
        else if (name == PooledObjectName.Lava)
        {
            return GetNewObject(PooledObjectName.Lava);
        }
        else if (name == PooledObjectName.DnaHp)
        {
            return GetNewObject(PooledObjectName.DnaHp);
        }
        else if (name == PooledObjectName.SphereDoubleJump)
        {
            return GetNewObject(PooledObjectName.SphereDoubleJump);
        }
        else if (name == PooledObjectName.SphereTimeAcceleration)
        {
            return GetNewObject(PooledObjectName.SphereTimeAcceleration);
        }
        else if (name == PooledObjectName.SphereTimeDilation)
        {
            return GetNewObject(PooledObjectName.SphereTimeDilation);
        }
        else if (name == PooledObjectName.DnaInv)
        {
            return GetNewObject(PooledObjectName.DnaInv);
        }
        
        return GetNewObject(PooledObjectName.DnaXp);
    }
    #endregion
    
    #region Returns

    public static void ReturnPlatform(GameObject obj)
    {
        ReturnPooledObject(PooledObjectName.Platform, obj);
    }
    
    public static void ReturnDnaXp(GameObject obj)
    {
        ReturnPooledObject(PooledObjectName.DnaXp, obj);
    }
    
    public static void ReturnDnaHp(GameObject obj)
    {
        ReturnPooledObject(PooledObjectName.DnaHp, obj);
    }    
    public static void ReturnDnaInv(GameObject obj)
    {
        ReturnPooledObject(PooledObjectName.DnaInv, obj);
    }
    public static void ReturnDJ(GameObject obj)
    {
        ReturnPooledObject(PooledObjectName.SphereDoubleJump, obj);
    }
    public static void ReturnTA(GameObject obj)
    {
        ReturnPooledObject(PooledObjectName.SphereTimeAcceleration, obj);
    }
    public static void ReturnTD(GameObject obj)
    {
        ReturnPooledObject(PooledObjectName.SphereTimeDilation, obj);
    }
    
    public static void ReturnLava(GameObject obj)
    {
        ReturnPooledObject(PooledObjectName.Lava, obj);
    }

    private static void ReturnPooledObject(PooledObjectName name, GameObject obj)
    {
        //obj.SetActive(false);
        pools[name].Add(obj);
    }
    #endregion
    
    #region Create

    /// <summary>
    /// Gets a new object
    /// </summary>
    /// <returns>new object</returns>
    private static GameObject GetNewObject(PooledObjectName name)
    {
        GameObject obj;
        if (name == PooledObjectName.Platform)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Platforms/Platform"));
        }
        /*else if (name == PooledObjectName.DnaXp)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/DNA/DnaXp"));
        }*/
        else if (name == PooledObjectName.DnaHp)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/DNA/DnaHp"));
        } 
        else if (name == PooledObjectName.DnaInv)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/DNA/DnaInv"));
        }
        
        else if (name == PooledObjectName.Lava)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Obstacles/lava"));
        }
        
        else if (name == PooledObjectName.SphereDoubleJump)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Buffs/DoubleJump"));
        }
        else if (name == PooledObjectName.SphereTimeAcceleration)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Buffs/TimeAcceleration"));
        }
        else if (name == PooledObjectName.SphereTimeDilation)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Buffs/TimeDilation"));
        }

        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/DNA/DnaXp"));
        }
        
        obj.SetActive(false);
        return obj;
    }

    #endregion
}
