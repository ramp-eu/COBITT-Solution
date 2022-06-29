# COBITT

<p>This tutorial introduces the usage of the WEB UI of COBITT Project and wires up the IOT devices throught the IOT AGENT and provides a simple UI to manage your orders and IOT devides.</p>


<h1>
  What is COBITT 
</h1>

<p>
  The COBITT WEB UI is a simple website that gives you the ability to create Services, Subscriptions, IOT Devices using the FIWARE IOT AGENT, gives you the ability to handle your devices either this is done manually or automatic for an order.
</p>

<h1>
  Common Functionality
</h1>

<h2>
  Services
</h2>

<p>
  Invoking group provision is always the the first step in connecting devices since it is always necessary to
supply an authentication key with each measurement and the IoT Agent will not initially know which URL
the context broker is responding on.
</p>

<p>
  Example of a service created in COBITT:
</p>
  
<pre>
  {
  "services": [
    {
      "apikey": "4jggokgpepnvsb2uv4s40d59ov",
      "cbroker": "http://orion:1026",
      "entity_type": "Thing",
      "resource": "/iot/json"
    }
  ]
}
</pre>

![screencapture-localhost-44393-Services-2022-06-29-07_07_25](https://user-images.githubusercontent.com/15981121/176349467-8475d806-2ed0-42ae-9891-e9a7d86cc4d8.png)


