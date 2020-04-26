using GameFramework.Resource;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityAsyncAwaitUtil;
using UnityEngine;
using UnityGameFramework.Runtime;
using static IEnumeratorAwaitExtensions;

namespace UGFExtensions
{
    public static class AsyncLoadAssetExtensions
    {
        public static IEnumerator LoadAssetAsync(this ResourceComponent resourceComponent, string assetName)
        {
            AsyncLoadAssetTag asyncLoadAssetTag = AsyncLoadAssetTag.Create();

            LoadAssetCallbacks loadAssetCallbacks = new LoadAssetCallbacks((string _assetName, object asset, float duration, object userData) =>
            {
                asyncLoadAssetTag.Asset = asset;
                asyncLoadAssetTag.LoadAssetState = LoadAssetState.Success;


            }, (string _assetName, LoadResourceStatus status, string errorMessage, object userData) =>
            {
                asyncLoadAssetTag.Asset = null;
                asyncLoadAssetTag.LoadAssetState = LoadAssetState.Failure;

            });

            resourceComponent.LoadAsset(assetName, null, 0, loadAssetCallbacks, null);


            while (asyncLoadAssetTag.LoadAssetState == LoadAssetState.Loading)
            {
                yield return null;
            }

            yield return asyncLoadAssetTag.Asset;
        }

        public static Task<object> LoadAssetAsync2(this ResourceComponent resourceComponent, string assetName)
        {
            TaskCompletionSource<object> loadAssetTcs = new TaskCompletionSource<object>();

            LoadAssetCallbacks loadAssetCallbacks = new LoadAssetCallbacks((string _assetName, object asset, float duration, object userData) =>
            {
                loadAssetTcs.SetResult(asset);

            }, (string _assetName, LoadResourceStatus status, string errorMessage, object userData) =>
            {

                loadAssetTcs.SetException(new Exception(errorMessage));

            });

            resourceComponent.LoadAsset(assetName, null, 0, loadAssetCallbacks, null);


            return loadAssetTcs.Task;
        }

        public static AsyncLoadAssetRequest LoadAssetAsync3(this ResourceComponent resourceComponent, string assetName)
        {
            AsyncLoadAssetRequest asyncLoadAssetRequest = AsyncLoadAssetRequest.Create();

            LoadAssetCallbacks loadAssetCallbacks = new LoadAssetCallbacks((string _assetName, object asset, float duration, object userData) =>
            {
                asyncLoadAssetRequest.Asset = asset;
                asyncLoadAssetRequest.LoadAssetState = LoadAssetState.Success;

            }, (string _assetName, LoadResourceStatus status, string errorMessage, object userData) =>
            {
                asyncLoadAssetRequest.Asset = null;
                asyncLoadAssetRequest.LoadAssetState = LoadAssetState.Failure;


            });

            resourceComponent.LoadAsset(assetName, null, 0, loadAssetCallbacks, null);

            return asyncLoadAssetRequest;
        }

        //public static SimpleCoroutineAwaiter<object> GetAwaiter(this AsyncLoadAssetRequest instruction)
        //{
        //    Debug.Log("GetAwaiter1");
        //    var awaiter = new SimpleCoroutineAwaiter<object>();
        //    RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
        //        AssetRequest(awaiter, instruction)));
        //    Debug.Log("GetAwaiter2");
        //    return awaiter;
        //}

        //public static IEnumerator AssetRequest(
        //    SimpleCoroutineAwaiter<object> awaiter, AsyncLoadAssetRequest instruction)
        //{
        //    Debug.Log("AssetRequest1");
        //    yield return instruction;
        //    Debug.Log("AssetRequest2");
        //    awaiter.Complete(instruction.Asset, null);
        //}

        static void RunOnUnityScheduler(Action action)
        {
            if (SynchronizationContext.Current == SyncContextUtil.UnitySynchronizationContext)
            {
                action();
            }
            else
            {
                SyncContextUtil.UnitySynchronizationContext.Post(_ => action(), null);
            }
        }
    }

}


