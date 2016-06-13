/*
select top 10 * from hours
*/

select sum(h.hours), sum(h.hours * h.rate) amount, c.FirstName, c.LastName
from hours h
join Consultants c on c.ConsultantId = h.ConsultantId
where h.billable = 1 /*and c.Active = 1*/ and c.lastname <> '' and h.HourDate > '1/1/2015'
group by c.FirstName, c.LastName
order by 1 desc

