select b.type_name, count(1) NumRows
from [sf_version_chnges] a
join [sf_vesion_items] as b on (a.[item_id] = b.[id])
group by b.type_name
order by 2 desc
