namespace Blog.ViewModels
{
    public class ResultViewModel<T>
    {
        //Esse T pode ser qualquer coisa List UM tipo Category só List<Category>
        public T Data { get; private set; } // T data é tipo Category qualquer um, Nesse caso server se Der Certo a requisição Mostra os dados
        public List<string> Errors { get; private set; } = new();

        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(T data)
        {
            Data = data;
        }

        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }

        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }
    }
}
