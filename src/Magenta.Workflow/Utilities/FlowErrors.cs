namespace Magenta.Workflow.Utilities
{
    internal class FlowErrors
    {
        #region General

        public const string ITEM_NOTFOUND = "{0} not found.";
        public const string SERVICE_ISNULL = "{0} is null.";
        public const string SERVICE_ISREQUIRED = "{0} must have a value.";
        public const string SERVICE_ISEMPTY = "{0} is empty.";
        public const string SERVICE_NOTRESOLVED = "service with name {0} could not be resolved.";
        public const string ERROR_OCCURED = "an error happended, to resolve the problem check the logs.";

        #endregion General

        #region Validations

        public const string INSTANCE_IS_INACTIVE = "inactive instance cannot move.";
        public const string MOVE_IMPOSSIBLE_TRANSITION = "move from this source '{0}' to target destination ''{1} is impossible, transition might not be defined.";

        #endregion Validations

        #region Reports

        public const string INSTANCE_HASNOSTEP = "instance has no steps, it might be broken or not completely created.";

        #endregion Reports

        #region Internal Phrases

        public const string STATE_NULL = "start:null";

        #endregion Internal Phrases
    }
}
