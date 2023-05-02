(function () {

async function start() {
	const server = "https://xyz.azurewebsites.net";
	
	const take = 50;
	const taskType = "Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTaskImpl";
	//const taskType = "SitefinityWebApp.Tasks.WorkdaySyncTask";
	
	var response = await fetch(`${server}/restapi/sitefinity/scheduling/scheduled-tasks?format=json&orderBy=0&skip=0&take=${take}&searchTerm=&filterBy=4`);
	var json = await response.json();
	console.log(`Found ${json.Items.length}`);
	
	for (var i = 0; i < json.Items.length; i++) {
		var item = json.Items[i];
		if(item.Name === taskType){
			console.log(`Item ${i+1} is ${taskType}. Deleting...`);
			await fetch(`${server}/restapi/sitefinity/scheduling/scheduled-tasks?taskId=${item.TaskId}&operation=3&hash=${item.Hash}`, { "method": "PUT" });
			await sleep(1000);
		} else {
			console.log(`Item ${i+1} is ${item.Name}. Skipping...`);
		}
	}
	
	console.log("Done");
}

function sleep(ms) {
	return new Promise(resolve => setTimeout(resolve, ms));
}

start();

})();
