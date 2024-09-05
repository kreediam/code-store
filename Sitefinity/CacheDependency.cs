namespace A
{
    public static class B
    {
        public static void C()
        {
            // Reschedule tasks when config changes, in case schedules change
            Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof(MyConfig), (ICacheDependencyHandler caller, Type trackedItemType, string trackedItemKey) =>
            {
                TaskScheduler.Schedule<MyTask>();
            });
        }
    }
}
