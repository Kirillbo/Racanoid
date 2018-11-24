using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tools
{
  
    public static class Tools
    {
        public static GameObject SetTransform(this GameObject obj, Vector3 initPosition, Quaternion initRotation, Transform parent, bool setActive = true)
        {
            obj.transform.position = initPosition;
            obj.transform.rotation = initRotation;
            
            obj.transform.SetParent(parent);
            
            if (setActive)
            {
                obj.SetActive(true);
            }
            return obj;
        }

        public static GameObject SetTransform(this GameObject obj, Vector3 initPosition, Quaternion initRotation, Vector3 scale, bool setActive = true)
        {
            obj.transform.position = initPosition;
            obj.transform.rotation = initRotation;
            obj.transform.localScale = scale;
            
            if (setActive)
            {
                obj.SetActive(true);
            }
            return obj;
        }
        
        
    }

    /// <summary>
    /// Создание массива случайных чисел
    /// </summary>
    public static class ToolsRandom
    {
        public static T[] Shuffle<T> (this T[] arr)
        {
            for (int i = 0; i < arr.Length; i++) {
                
                T temp = arr[i];
                int randomIndex = Random.Range(0, arr.Length);
                arr[i] = arr[randomIndex];
                arr[randomIndex] = temp;
            }

            return arr;
        }
        
        public static float[] GenerateArrayRandom(int count, float min, float max)
        {
            float[] arr = new float[count];
            int index = 0;

            while (index < count)
            {
                arr[index] = UnityEngine.Random.Range(min, max);
                index++;
            }
            return arr;
        }

        public static bool Choice(float chance)
        {
            float[] arrChose = {1 - chance, chance};
            var c = Choose(arrChose);

            return c == 1 ? true : false;

        }
        
        public static float Choose(float[] probs)
        {

            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }

        /// <summary>
        /// рандомный знак
        /// </summary>
        /// <returns></returns>
        public static int RandomSign()
        {
            return Random.Range(0, 2) * 2 - 1;
        }


    }
}
