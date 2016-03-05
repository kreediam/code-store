-first day of month
select dateadd(mm, datediff(mm, 0, getdate()), 0)

-first day of last month
select dateadd(mm, datediff(mm, 0, dateadd(mm, -1, getdate())), 0)

-monday of the current week
select dateadd(wk, datediff(wk, 0, getdate()), 0)

-sunday of the current week
set datefirst 1
select dateadd(dd, 1 - datepart(dw, getdate()), getdate())

-first day of the year
select dateadd(yy, datediff(yy, 0, getdate()), 0)

-first day of the quarter
select dateadd(qq, datediff(qq, 0, getdate()), 0)

-midnight for the current day
select dateadd(dd, datediff(dd, 0, getdate()), 0)

-last day of prior month
select dateadd(ms, -3, dateadd(mm, datediff(mm, 0, getdate()), 0))

-last day of prior year
select dateadd(ms, -3, dateadd(yy, datediff(yy, 0, getdate()), 0))

-last day of current month
select dateadd(ms, -3, dateadd(mm, datediff(m, 0, getdate()) + 1, 0))

-last day of current year
select dateadd(ms, -3, dateadd(yy, datediff(yy, 0, getdate()) + 1, 0))

-first monday of the month
select dateadd(wk, datediff(wk, 0, dateadd(dd, 6 - datepart(day, getdate()), getdate())), 0)     
