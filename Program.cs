using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        TextEditor textEditor = new TextEditor();
        TextEditorHistory history = new TextEditorHistory();

        // Initial state
        textEditor.Text = "Hello, World!";
        history.SaveState(textEditor.CreateMemento());

        // Modify the text
        textEditor.Text = "Modified text.";
        history.SaveState(textEditor.CreateMemento());

        // Undo to previous state
        Memento previousState = history.Undo();
        if (previousState != null)
        {
            textEditor.RestoreMemento(previousState);
        }
        else
        {
            Console.WriteLine("No more undo history.");
        }
    }
}

// Originator
class TextEditor
{
    private string _text;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            Console.WriteLine("Text set to: " + _text);
        }
    }

    public Memento CreateMemento()
    {
        return new Memento(_text);
    }

    public void RestoreMemento(Memento memento)
    {
        _text = memento.TextState;
        Console.WriteLine("Restored text: " + _text);
    }
}

// Memento
class Memento
{
    public string TextState { get; }

    public Memento(string text)
    {
        TextState = text;
    }
}

// Caretaker
class TextEditorHistory
{
    private Stack<Memento> _history = new Stack<Memento>();

    public void SaveState(Memento memento)
    {
        _history.Push(memento);
    }

    public Memento Undo()
    {
        if (_history.Count > 0)
        {
            return _history.Pop();
        }
        return null;
    }
}
