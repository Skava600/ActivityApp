using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepProject.Cmds
{
    public class RelayCommandT<T> : RelayCommand
    {
        private readonly Action<T?> _execute;
        private readonly Func<T?, bool>? _canExecute;

        public RelayCommandT(Action<T?> execute) : this(execute, null) { }

        public RelayCommandT(Action<T?> execute, Func<T?, bool>? canExecute)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public override bool CanExecute(object? parameter) => _canExecute == null || _canExecute((T?) parameter);

        public override void Execute(object? parameter) { _execute((T?)parameter); }
    }
}
