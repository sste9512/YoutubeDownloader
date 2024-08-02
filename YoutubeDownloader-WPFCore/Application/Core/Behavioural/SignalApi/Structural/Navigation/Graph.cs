namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Navigation;

public class Graph
{
    private Dictionary<int, object> _graph = new();


    public void Add(int key, object value)
    {
        _graph.Add(key, value);
    }

    public void Clear()
    {
        _graph.Clear();
    }

    public bool Remove(int key)
    {
        return _graph.Remove(key);
    }
}