        /// <summary>
        /// Split a collection into near-equal-sized pages.
        /// In the event of an uneven number, pages 1 - {pages-1} could have one more item than the last page
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="page">Zero-based page number</param>
        /// <param name="pages">Total number of pages</param>
        /// <returns></returns>
        public static IEnumerable<T> EqualSplit<T>(this IEnumerable<T> source, int page, int pages)
        {
            var length = source.Count();
            int pageSize = length / pages;

            if (length % pages > page)
            {
                pageSize++;
            }

            var start = (length / pages) * page;

            if (length % pages > 0)
            {
                start = start + Math.Min(length % pages, page);
            }

            return source.Skip(start).Take(pageSize);
        }
