//Cancellation Token Source
CancellationTokenSource cancellationTokenSource;
cancellationTokenSource.Cancel();
cancellationTokenSource.Dispose();

cancellationTokenSource.CancelAfter(5000);
...
cancellationTokenSource.Dispose();

//Cancellation Token
Task.Run(() =>
{
	if (token.IsCancellationRequested) { }
});

//Cancellation Token
CancellationTokenSource cancellationTokenSource;
CancellationToken token = cancellationTokenSource.Token;
Task.Run(() => { }, token);
Task.Run(() =>
{
	if (token.IsCancellationRequested) { }
});

//Cancellation
CancellationTokenSource cancellationTokenSource;
CancellationToken token = cancellationTokenSource.Token;
cancellationTokenSource.Cancel();
Task.Run(() => { }, token)

//Cancellation Token and ContinueWith

CancellationTokenSource cancellationTokenSource;
CancellationToken token = cancellationTokenSource.Token;
var task = Task.Run(() => { }, token);
task.ContinueWith((t) => { }, token);

//Task Status
var token = new CancellationToken(canceled: true);
var task = Task.Run(() => "Won’t even start", token);
task.ContinueWith((completingTask) =>
{
	// completingTask.Status = Canceled
});
task.ContinueWith((completingTask2) =>
{
	// completingTask2.Status = Canceled
});

var token = new CancellationToken(canceled: true);
var task = Task.Run(() => "Won’t even start", token);
task.ContinueWith((completingTask) =>
{
	// completingTask.Status = Canceled
})
.ContinueWith((continuationTask) =>
{
	// continuationTask.Status = RanToCompletion
});

