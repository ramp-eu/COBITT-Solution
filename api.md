<h1 dir="auto"><a id="user-content-api" class="anchor" aria-hidden="true" href="#api"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>API</h1>



<h2 dir="auto"><a id="user-content---services" class="anchor" aria-hidden="true" href="#--services"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>
  Services
</h2>

<p dir="auto">
  Example of a service created in COBITT:
</p>

<pre>{
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

<h2 dir="auto"><a id="user-content---subscriptions" class="anchor" aria-hidden="true" href="#--subscriptions"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>
  Subscriptions
</h2>

<p dir="auto">
  Example of a subscription created in COBITT:
</p>

<pre>curl -iX POST \
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

<h2 dir="auto"><a id="user-content---create-an-iot-device" class="anchor" aria-hidden="true" href="#--create-an-iot-device"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>
  Create an IOT Device
</h2>

<p dir="auto">
  Example of an IOT Device creation in COBITT:
</p>

<pre>{
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


<h2 dir="auto"><a id="user-content---manual-operation" class="anchor" aria-hidden="true" href="#--manual-operation"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>
  IOT Device Operation
</h2>

<p>Start Machine</p>

<pre>{
  "actionType":"update",
  "entities":[
    {
      "id":"urn:ngsi-ld:Axis:001", "type":"Axis",
      "start":{"type":"command", "value": ""}
    }
  ]
}
</pre>

<p>Stop Machine</p>

<pre>{
  "actionType":"update",
  "entities":[
    {
      "id":"urn:ngsi-ld:Axis:001", "type":"Axis",
      "stop":{"type":"command", "value": ""}
    }
  ]
}
</pre>

<p dir="auto">
 Example of the Simulation to update the Status Attribute of the device: 
</p>

<pre>{
  "actionType":"update",
  "entities":[
    {
      "id":"urn:ngsi-ld:Axis:001", "type":"Axis",
      "Status":{"type":"Text", "value": "Cutting &amp; Stacking Complete"}
    }
  ]
}
</pre>

