using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Animations;
// using UnityEditor;

public class ScriptTimeline : MonoBehaviour
{
    private List<AnimationClip> DefineClipsOld()
    {
        var clips = new List<AnimationClip>();
        var aclip0 = new AnimationClip();
        aclip0.name = "Aclip 0 ";
        // https://docs.unity3d.com/ScriptReference/AnimationClip.SetCurve.html
        aclip0.SetCurve("", typeof(Transform), "localPosition.x", AnimationCurve.EaseInOut(0, 0, 4, 10));
        aclip0.SetCurve("", typeof(Transform), "localPosition.y", AnimationCurve.EaseInOut(0, 0, 4, 2.5f));
        aclip0.SetCurve("", typeof(Transform), "localPosition.z", AnimationCurve.EaseInOut(0, 0, 4, 2));
        clips.Add(aclip0);

        var aclip1 = new AnimationClip();
        aclip1.name = "Aclip 1 ";
        aclip1.SetCurve("", typeof(Transform), "localPosition.x", AnimationCurve.EaseInOut(0, 10, 8, 0));
        aclip1.SetCurve("", typeof(Transform), "localPosition.y", AnimationCurve.EaseInOut(0, 2.5f, 8, 0));
        aclip1.SetCurve("", typeof(Transform), "localPosition.z", AnimationCurve.EaseInOut(0, 2, 8, 0));
        clips.Add(aclip1);

        return clips;
    }

    private List<AnimationClip> DefineClips()
    {
        var clips = new List<AnimationClip>();
        var aclip0 = new AnimationClip();
        aclip0.name = "Aclip 0 ";
        // https://docs.unity3d.com/ScriptReference/AnimationClip.SetCurve.html
        var tseg = 2f;
        var curvPx = new AnimationCurve();
        curvPx.AddKey(new Keyframe(0*tseg, 0));
        curvPx.AddKey(new Keyframe(1*tseg, 8));
        curvPx.AddKey(new Keyframe(2*tseg, 0));
        curvPx.AddKey(new Keyframe(3*tseg,-8));
        curvPx.AddKey(new Keyframe(4*tseg, 0));
        var curvPy = new AnimationCurve();
        curvPy.AddKey(new Keyframe(0*tseg, 8));
        curvPy.AddKey(new Keyframe(4*tseg, 8));
        var curvPz = new AnimationCurve();
        curvPz.AddKey(new Keyframe(0*tseg, 8));
        curvPz.AddKey(new Keyframe(1*tseg, 0));
        curvPz.AddKey(new Keyframe(2*tseg,-8));
        curvPz.AddKey(new Keyframe(3*tseg, 0));
        curvPz.AddKey(new Keyframe(4*tseg, 8));

        var curvRx = new AnimationCurve();
        curvRx.AddKey(new Keyframe(0*tseg,20));
        curvRx.AddKey(new Keyframe(4*tseg,20));
        var curvRy = new AnimationCurve();
        curvRy.AddKey(new Keyframe(0*tseg,180));
        curvRy.AddKey(new Keyframe(1*tseg,270));
        curvRy.AddKey(new Keyframe(2*tseg,360));
        curvRy.AddKey(new Keyframe(3*tseg,450));
        curvRy.AddKey(new Keyframe(4*tseg,540));
        var curvRz = new AnimationCurve();
        curvRz.AddKey(new Keyframe(0*tseg, 0));
        curvRz.AddKey(new Keyframe(4*tseg, 0));


        aclip0.SetCurve("", typeof(Transform), "localPosition.x", curvPx);
        aclip0.SetCurve("", typeof(Transform), "localPosition.y", curvPy);
        aclip0.SetCurve("", typeof(Transform), "localPosition.z", curvPz);

        aclip0.SetCurve("", typeof(Transform), "localEulerAngles.x", curvRx);
        aclip0.SetCurve("", typeof(Transform), "localEulerAngles.y", curvRy);
        aclip0.SetCurve("", typeof(Transform), "localEulerAngles.z", curvRz);
        clips.Add(aclip0);

        return clips;
    }



    private void Awake()
    {

        var clips = DefineClips();


        var cubego = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cubego.name = "Cubego";
        cubego.transform.localScale = new Vector3(2, 2, 2);
        var cam = cubego.AddComponent<Camera>();
        cam.targetDisplay = 3;
        cubego.AddComponent<Animator>();

        GameObject directorGO = new GameObject("Timeline");
        //directorGO.AddComponent<CutsceneDirector>();
        PlayableDirector director = directorGO.AddComponent<PlayableDirector>();
        PlayableGraph graph = PlayableGraph.Create();

        TimelineAsset timeline = ScriptableObject.CreateInstance<TimelineAsset>();
        AnimationTrack track = timeline.CreateTrack<AnimationTrack>("NewTrack"); ;

        for (int i = 0; i < clips.Count; ++i)
        {
            AnimationClip clip =clips[i];
            var output = AnimationPlayableOutput.Create(graph, clip.name, cubego.GetComponent<Animator>());
            var playable = AnimationClipPlayable.Create(graph, clip);
            output.SetSourcePlayable<AnimationPlayableOutput, AnimationClipPlayable>(playable);

            TimelineClip timelineClip = track.CreateClip(clip);
            timelineClip.duration = clip.averageDuration;
            timelineClip.displayName = clip.name;

            director.SetGenericBinding(track, cubego);
        }

        //AssetDatabase.CreateAsset(timeline, "Assets/TestTimeline.playable");
        director.playableAsset = timeline;
        // 4. Play the timeline
        director.Play();
    }

}
