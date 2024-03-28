        private void ListenForConfigChanges()
        {
            // Reschedule tasks when MyConfig changes, in case schedules change
            CacheDependency.Subscribe(typeof(MyConfig), (ICacheDependencyHandler caller, Type trackedItemType, string trackedItemKey) =>
            {
                Log.Write($"Scheduled tasks: Cache dependency callback fired for MyConfig. Rescheduling scheduled tasks...", ConfigurationPolicy.Trace);
                ScheduleTasks();
            });
        }