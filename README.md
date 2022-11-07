# COBITT

<p>This tutorial introduces the usage of the WEB UI of COBITT Project and wires up the IOT devices throught the IOT AGENT and Orion Context Broker and provides a simple UI to manage your orders and IOT devices.</p>

<p>
  The COBITT WEB UI is a simple website that gives you the ability to create Services, Subscriptions, IOT Devices using the FIWARE IOT AGENT, gives you the ability to handle your devices either this is done manually or automatic for an order.
</p>

<p dir="auto">This project is part of <a href="http://www.dih-squared.eu/" rel="nofollow">DIH^2</a>. For more information check the RAMP Catalogue entry for the
<a href="https://github.com/ramp-eu">components</a>.</p>

<table>
<thead>
<tr>
<th><g-emoji class="g-emoji" alias="books" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f4da.png">📚</g-emoji> <a href="#usage" rel="nofollow">Documentation</a></th>
<th><g-emoji class="g-emoji" alias="whale" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f433.png">🐳</g-emoji> <a href="https://hub.docker.com/r/iasonasiasonos/dih2_webapp" rel="nofollow">Docker Hub</a></th>
<th><g-emoji class="g-emoji" alias="tv" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f4fa.png">📺</g-emoji> <a href="https://youtu.be/z5BjUznFawQ" rel="nofollow">Step by Step</a></th>
</tr>
</thead>
</table>

<h2 dir="auto"><a id="user-content-contents" class="anchor" aria-hidden="true" href="#contents"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>Contents</h2>

<ul dir="auto">
<li><a href="#background">Background</a></li>
<li><a href="#install">Install</a></li>
<li><a href="#usage">Usage</a></li>
<li><a href="#getting-started">Getting started</a></li>
<li><a href="#api">API</a></li>
<li><a href="#license">License</a></li>
</ul>

<h2 dir="auto"><a id="user-content-background" class="anchor" aria-hidden="true" href="#background"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>Background</h2>

<p>Background Information</p>

<h2 dir="auto"><a id="user-content-install" class="anchor" aria-hidden="true" href="#install"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>Install</h2>

<p dir="auto">Information about how to install the COBITT ROSE-AP can be found at the corresponding section of the <a href="https://github.com/iasonasiasonos/COBITT_SOLUTION">Installation Guide</a>.</p>


<h1 id="usage">
  Usage
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


<h2>
  Subscriptions
</h2>

<p>
  Persisting Context is another funcionality of COBITT. The system uses <a href="https://fiware-cygnus.readthedocs.io/en/latest/">Cygnus</a> and MySQL to persist data. Cygnus is a connector in charge of persisting certain sources of data in certain configured third-party storages, creating a historical view of such data.
</p>

<p>
  The system also creates a subscription for each IOT Device in the IOT Device Creation section which allows the system to hear for changes from the IOT Devices and receive these changes in the system to update the database and the UI.
</p>

<p>
  Example of a subscription created in COBITT:
</p>

<pre>
curl -iX POST \
  'http://localhost:1026/v2/subscriptions' \
  -H 'Content-Type: application/json' \
  -H 'fiware-service: openiot' \
  -H 'fiware-servicepath: /' \
  -d '{
  "description": "Notify Cygnus of all context changes",
  "subject": {
    "entities": [
      {
        "idPattern": ".*"
      }
    ]
  },
  "notification": {
    "http": {
      "url": "http://cygnus:5050/notify"
    },
    "attrsFormat": "legacy"
  },
  "throttling": 5
}'
</pre>

![screencapture-localhost-44393-Subscriptions-2022-06-29-07_13_12](https://user-images.githubusercontent.com/15981121/176350128-bd039e70-8e1a-4840-a92b-0dae3319bb5c.png)


<h2>
  IOT Devices
</h2>

<p>
  This section allows you to view your existing/provisioned IOT Devices in Orion Context Provider. It lists all provisioned devices by making a GET request to the /iot/devices endpoint.

The response includes all the commands and attributes mappings associated with all provisioned IoT devices.
</p>

![screencapture-localhost-44393-IOTDevices-2022-06-29-07_21_34](https://user-images.githubusercontent.com/15981121/176351010-d0b0bc5b-3403-4176-ad36-beb595f77ed9.png)



<h2>
  Create an IOT Device
</h2>

<p>
  The system provides the functionality to create as many IOT Devices as you want. You just need to enter the correct details and the endpoint that the IOT Device is listening to.
  
  You can create 3 different types of IOT Devices in the system:
  <ol>
    <li>
      3-Axis
    </li>
    <li>
      5-Axis
    </li>
    <li>
      ROS2
    </li>
  </ol>
</p>

<p>
  Example of an IOT Device creation in COBITT:
</p>

<p>
  The system uses the Provision a Sensor API of the IOT Agent and uses the URNs following the NGSI-LD specification when creating entities. It uses the attributes that are active readings from the device and an endpoint attribute holds
the location where the IoT Agent needs to send the UltraLight command and the commands array includes a list of each command that can be invoked. 
</p>

<pre>
{
   "device_id":"ros001",
   "service":"openiot",
   "service_path":"/",
   "entity_name":"urn:ngsi-ld:Ros:001",
   "entity_type":"Ros",
   "endpoint":"https://dihweb.conveyor.cloud/api/api/RosEndpoint",
   "transport":"HTTP",
   "attributes":[
      {
         "object_id":"Position",
         "name":"Position",
         "type":"Text"
      },
      {
         "object_id":"Status",
         "name":"Status",
         "type":"Text"
      },
      {
         "object_id":"CurrentStation",
         "name":"CurrentStation",
         "type":"Text"
      },
      {
         "object_id":"CurrentRoute",
         "name":"CurrentRoute",
         "type":"Text"
      },
      {
         "object_id":"ObstacleDetected",
         "name":"ObstacleDetected",
         "type":"Text"
      },
      {
         "object_id":"BatteryLevel",
         "name":"BatteryLevel",
         "type":"Text"
      }
   ],
   "commands":[
      {
         "object_id":"start",
         "name":"start",
         "type":"command"
      },
      {
         "object_id":"stop",
         "name":"stop",
         "type":"command"
      },
      {
         "object_id":"auto",
         "name":"auto",
         "type":"command"
      }
   ],
   "protocol":"PDI-IoTA-UltraLight",
   "explicitAttrs":false
}
</pre>

![screencapture-localhost-44393-IOTDevices-Create-2022-06-29-07_24_57](https://user-images.githubusercontent.com/15981121/176351370-54ead41c-3c73-47e6-bbef-f8dfcdae2689.png)


<h1>
  Order Management
</h1>

<p>
  The system provides to the user the functionality of handling the devices, the manual operation of the IOT Devices and an ordering management.
</p>


<h2>
  Manage Devices
</h2>

<p>
  From this section you can manage your devises, either you delete them from the system or reorder them for the automation part.
</p>

![screencapture-localhost-44393-ManageDevices-2022-06-29-07_47_07](https://user-images.githubusercontent.com/15981121/176353839-e7f2b779-2372-4a15-8242-169c400c04b4.png)

<h2>
  Manual Operation
</h2>

<p>
  Here, all the created IOT Devices will be listed with the ability to:
   <ol>
    <li>
      Manual Start of the IOT Device
    </li>
    <li>
      Manual Stop of the IOT Device
    </li>
    <li>
      View the active readings attributes from the device
    </li>
  </ol>
</p>

<p>
  The Simulate Attributes Button simulates how an actual device will send these attributes to the Orion Context Broker and update the data model with the latest data. 
</p>
  
<p>
 Example of the Simulation to update the Status Attribute of the device: 
</p>

<pre>
{
  "actionType":"update",
  "entities":[
    {
      "id":"urn:ngsi-ld:Axis:001", "type":"Axis",
      "Status":{"type":"Text", "value": "Cutting & Stacking Complete"}
    }
  ]
}
</pre>


![screencapture-localhost-44393-ManualOperation-2022-06-29-07_48_09](https://user-images.githubusercontent.com/15981121/176353944-85ca3fc2-cff9-4786-beea-8fb628d74c4c.png)


<h2>
  Orders
</h2>

<p>
 From the Orders the user can add a new order containing the following fields:
  Based on the Height and Weight, the system will pass some parameters to the IOT Devices in order to execute the order correctly.
  <ol>
    <li>
      Order Name
    </li>
    <li>
      Height
    </li>
    <li>
      Weight
    </li>
     <li>
      Order Date
    </li>
     <li>
      Order Status
    </li>
  </ol>
</p>


![screencapture-localhost-44393-Orders-2022-06-29-08_03_13](https://user-images.githubusercontent.com/15981121/176355633-470d80a6-9df6-44d5-a5bf-9bbdab4bbac8.png)


<h2>
  Manage Order
</h2>


<p>
 The order can be started by pressing the button "Start Order" and the process will start sending commands and parameters to the IOT Devices until the process is finished.
  
The system offers a table that when the IOT Device will respond back to Orion Context Broker will be listed there as a feedback to the user to check the process of the order. 
</p>

![screencapture-localhost-44393-Orders-Manage-2022-06-29-09_18_01](https://user-images.githubusercontent.com/15981121/176365737-119d9307-49c4-4a42-bee1-97f3c6be0822.png)

<p>
After the process is completed the system will flag the order as finished. The users can then inspect the order and can decide if the order is succesfull or failed and with either option can be flagged in the system in order to update its statistics.
</p>

