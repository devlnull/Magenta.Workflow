namespace Magenta.Workflow.Utilities
{
    internal class FlowErrors
    {
        #region General

        public const string ItemNotFound = "{0} not found.";
        public const string ServiceIsNull = "{0} is null.";
        public const string ServiceIsRequired = "{0} must have a value.";
        public const string ServiceIsEmpty = "{0} is empty.";
        public const string ServiceNotResolved = "service with name {0} could not be resolved.";
        public const string ErrorOccurred = "an error happended, to resolve the problem check the logs.";

        #endregion General

        #region Validations

        public const string InstanceIsInactive = "inactive instance cannot move.";
        public const string MoveImpossibleTransition = "move from this source '{0}' to target destination ''{1} is impossible, transition might not be defined.";

        #endregion Validations

        #region Reports

        public const string InstanceHasnostep = "instance has no steps, it might be broken or not completely created.";

        #endregion Reports

        #region Internal Phrases

        public const string StateNull = "start:null";

        #endregion Internal Phrases
    }
}
