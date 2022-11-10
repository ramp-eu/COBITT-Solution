# COBITT ROSE-AP

<p>The <b>COBITT ROSE-AP</b> facilitates a dynamic and agile manufacturing process, supported by FIWARE, digital, and robotized infrastructure. In particular, it supports a fully automated and agile  process that involves the management, use, coordination, and monitoring of multiple  ROS2 robotic systems, CNC machines, and IoT devices.The main innovation/contribution of this ROSE-AP  to the FIWARE community lies in the development of a new IoT Agent that allows for connecting multiple ROS2 robotic systems simultaneously, using the IoT Agent nodelib and the rclnode.js  library.</p>

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

<p><b>Use Case Description</b></p>
<p>
COBITT aims to support companies that manufacture multi-layered composite and synthetic-material products, through laminate stacking and forming processes, by catering to a fully automated and agile manufacturing process. More precisely, it facilitates the optimal co-operation of heterogeneous machines and the FIWARE modules for interoperability, with the aim of achieving order completion.

These machines primarily include: Transportation Robotic Platforms that transport material and waste fabric material between factory workstations and designated factory areas; different types of  CNC machines (e.g., 3-axis cartesian robotic laser cutter with integrated pick and place robotic end-effector for precise cutting and stacking of the fabric material, 5-axis cartesian robotic laser  cutter/router for precise shape cutting and trimming).
</p>

<p>
<b>Proposed Solution</b>
</p>
<p>
<ul>
  <li>
  Seamless connection/communication between multiple heterogeneous devices (ROS2, IoT, CNC).
  </li>
  <li>
  Efficient management of factory operations via a user-friendly and intuitive Web interface.
  </li>
  <li>
  Real-time monitoring of the performance, availability and quality of factory workstations.
  </li>
</ul>

The architecture of the COBITT solution is illustrated below:

![thumbnail_Picture 1](https://user-images.githubusercontent.com/15981121/201098735-db4fdccd-c2de-4fcc-b512-fbbe726d264c.jpg)

The solution comprises the following components:

<b>COBITT-WEB-APP</b>: an ASP.NET CORE WEB Application that provides an interface for creating services, subscriptions and devices connected to the ORION Context Broker via an IoT Agent. It allows for managing all IoT and ROS 2 devices, manually interacting with them (e.g., options to Start and Stop.), and automatically creating a new job workflow.

<b>COBITT-SQL-DB</b>: a custom MSSQL database to hold all information on the orders placed (success/error information), the current state of the devices, the manual operation states, and all the KPIs tracked during operation.

<b>RAMP Dashboards (Superset)</b>: customizable dashboards for running data analytics and pulling up metrics that make sense of the data collected. The dashboards are used to monitor the real-time operation of the COBITT ROSE-AP, its overall performance over time, and in particular the Performance, Availability and Quality KPIs set.

<b>FIWARE-ORION-CONTEXT-BROKER</b>: an NGSIv2 server implementation for managing the entire lifecycle of context information including updates, queries, registrations, and subscriptions.

<b>FIWARE-IoT-AGENT</b>: a bridge that is used to communicate with the two (2) IoT devices, as well as the two (2) ROS 2 devices of the COBITT factory, through a simple JSON protocol and the ORION Context Broker. This protocol is based on simple single level JSON Objects codifying (attribute, value) pairs.

<ul>
  <li>
  To integrate the FIWARE IoT Agent for JSON with ROS 2 devices, a new ROS 2 Transport Binding has been implemented. This topic-based integration is realized using the rclnodejs library (i.e., the ROS 2 client for JavaScript) by creating a ROS 2 node that then joins the rest of the ROS 2 nodes, which interact with each other using topics.
  </li>
</ul>


<b>MONGO-DB</b>:  a database that is used by the ORION Context Broker to hold context data information such as data entities, subscriptions and registrations, as well as the IoT Agent to hold device information such as device URLs and Keys.

MYSQL-DB: a container that hosts the MySQL server, which is used to store historical data provided by FIWARE-CYGNUS. All information passed through the ORION Context Broker over time is stored in this MySQL Database.

<b>FIWARE-CYGNUS</b>: the connector in charge of persisting the aforementioned data in MYSQL-DB to create a historical view of the data.
</p>

<h2 dir="auto"><a id="user-content-install" class="anchor" aria-hidden="true" href="#install"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>Install</h2>

<p dir="auto">Information about how to install the COBITT ROSE-AP can be found at the corresponding section of the <a href="https://github.com/iasonasiasonos/COBITT_SOLUTION">Installation Guide</a>.</p>

<h2 dir="auto"><a id="user-content-getting-started" class="anchor" aria-hidden="true" href="#usage"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>Usage</h2>

<p dir="auto">It is recommended to start with the <a href="https://github.com/iasonasiasonos/COBITT/blob/master/stepbystep.md">Step by Step tutorial</a>.</p>

<h2 dir="auto"><a id="user-content-getting-started" class="anchor" aria-hidden="true" href="#getting-started"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>Getting started</h2>

<p dir="auto">It is recommended to start with the <a href="https://github.com/iasonasiasonos/COBITT/blob/master/stepbystep.md">Step by Step tutorial</a>.</p>

<h2 dir="auto"><a id="user-content-api" class="anchor" aria-hidden="true" href="#api"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>API</h2>

<p dir="auto">A simple API guide can be found <a href="https://github.com/iasonasiasonos/COBITT/blob/master/api.md">here</a>.</p>

<h2 dir="auto"><a id="user-content-license" class="anchor" aria-hidden="true" href="#license"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path></svg></a>License</h2>

<p dir="auto">Mobile Devices Laboratory</p>


