using System.Collections.Generic;

namespace Knight_survival.GameScene;

public class SceneManager
{
    private Stack<IScene> sceneStack;

    public SceneManager()
    {
        sceneStack = new();
    }

    public void AddScene(IScene scene)
    {
        scene.Load();
        sceneStack.Push(scene);
    }

    public void RemoveScene(IScene scene)
    {
        sceneStack.Pop();
    }

    public IScene GetCurrentScene()
    {
        return sceneStack.Peek();
    }
}