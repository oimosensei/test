using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;

public class ReactivePropertyTest : MonoBehaviour
{
    ReactiveCollection<bool> collection = new ReactiveCollection<bool>(new bool[] {false,false,false});
    Subject<int> nejiCount = new Subject<int>();
    IObservable<int> NejiCount => nejiCount;

    // Start is called before the first frame update
    void Start()
    {
        collection.ObserveReplace().Subscribe(x =>
        {
            int count = collection.Count(collected => collected);
            nejiCount.OnNext(count);
        });

        collection.ObserveEveryValueChanged(x => x.Count(collected => collected)) // コレクション内のtrueの数が変わるたびに発火
            .StartWith(collection.Count(collected => collected)) // 初期値を発火
            .Subscribe(nejiCount)
            .AddTo(this); // Dispose管理
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
