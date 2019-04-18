var Logger =
{
	info: function (message)
	{
		if (window.console && window.console.info)
		{
			window.console.info(message);
		}
	}
}

function copyToDataIn(event)
{
	document.getElementById("testForm").dataIn.value = document.getElementById("dataInExample").value;
}

function onSubmitXML(event)
{
	var testForm = document.getElementById("testForm"), testJavaScript = testForm.testJavaScript.checked,
		dataIn = testForm.dataIn.value, issueTimeStamp = testForm.issueTimeStamp.value,
		sharedSecret = testForm.sharedSecret.value;

	Logger.info("dataIn=");
	Logger.info(dataIn);
	Logger.info("issueTimeStamp=");
	Logger.info(issueTimeStamp);
	Logger.info("sharedSecret=");
	Logger.info(sharedSecret);

	var payload = dataIn + issueTimeStamp + sharedSecret;

	Logger.info("payload=");
	Logger.info(payload);

	var payloadHash = b64_sha1(payload);

	Logger.info("payloadHash=");
	Logger.info(payloadHash);

	testForm.hashValue.value = payloadHash;

	if (!testJavaScript)
	{
		testForm.submit();
	}
}