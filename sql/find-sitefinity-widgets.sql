select pn.title_, od.caption_, *
from sf_object_data od
join sf_page_node pn on od.page_id = pn.content_id
join sf_control_properties cp on cp.control_id = od.id
where object_type like '%Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy%'
and cp.nme = 'ControllerName'
and od.caption_ like '%StockWidget%'
