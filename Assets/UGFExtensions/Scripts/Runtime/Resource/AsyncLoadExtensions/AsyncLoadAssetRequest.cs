using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UGFExtensions
{
    public enum LoadAssetState
    {
        Init,
        Loading,
        Failure,
        Success,

    }

    public class AsyncLoadAssetRequest : CustomYieldInstruction
    {
        public object Asset;
        public LoadAssetState LoadAssetState = LoadAssetState.Init;

        public override bool keepWaiting
        {
            get { return !(LoadAssetState == LoadAssetState.Failure || LoadAssetState == LoadAssetState.Success); }
        }

        public static AsyncLoadAssetRequest Create()
        {
            AsyncLoadAssetRequest asyncLoadAssetRequest = new AsyncLoadAssetRequest
            {
                LoadAssetState = LoadAssetState.Loading
            };
            return asyncLoadAssetRequest;
        }
    }
}
