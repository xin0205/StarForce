using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGFExtensions
{
    public class AsyncLoadAssetTag
    {
        public object Asset;
        public LoadAssetState LoadAssetState = LoadAssetState.Init;

        public static AsyncLoadAssetTag Create()
        {
            AsyncLoadAssetTag asyncLoadAssetTag = new AsyncLoadAssetTag
            {
                LoadAssetState = LoadAssetState.Loading
            };
            return asyncLoadAssetTag;
        }
    }

    
}
