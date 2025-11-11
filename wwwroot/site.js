// site.js
console.log("✅ site.js cargado correctamente");

//window.raceHub = {
//  connection: null,

//  init: async function (raceId, dotnetRef) {
//    console.log("🔹 raceHub.init ejecutado con raceId:", raceId);

//    if (this.connection) {
//      console.warn("⚠️ Ya existe una conexión SignalR activa.");
//      return;
//    }

//    this.connection = new signalR.HubConnectionBuilder()
//      .withUrl(`${window.location.origin}/race-hub`)
//      .configureLogging(signalR.LogLevel.Information)
//      .withAutomaticReconnect()
//      .build();

//    // Evento: Recibir actualización desde el servidor
//    this.connection.on("ReceiveRaceUpdate", (update) => {
//      console.log("📡 Update recibido:", update);
//      if (dotnetRef) {
//        // invoca el método C# que pusiste
//        dotnetRef
//          .invokeMethodAsync("ReceiveRaceUpdateFromJS", update)
//          .catch((err) => console.error("Error invoking .NET method:", err));
//      }
//      // Mostrar directamente en la UI también
//      try {
//        this.renderUpdate(update);
//      } catch (e) {
//        console.error(e);
//      }
//    });

//    // Loguear eventos de ciclo de vida
//    this.connection.onreconnecting((error) => {
//      console.warn("SignalR: reconectando", error);
//      this.showMessage("Reconectando con el servidor...", "warning");
//    });

//    this.connection.onreconnected((connectionId) => {
//      console.log("SignalR: reconectado, connectionId=", connectionId);
//      this.showMessage("Reconectado.", "success");
//    });

//    try {
//      await this.connection.start();
//      console.log("✅ Conectado al servidor SignalR");

//      await this.connection.invoke("JoinRaceGroup", raceId);
//      console.log(`🟢 Unido al grupo race_${raceId}`);
//      this.showMessage(
//        `Conectado al seguimiento de carrera #${raceId}`,
//        "success"
//      );
//    } catch (err) {
//      console.error("❌ Error al conectar con SignalR:", err);
//      this.showMessage(
//        "Error al conectar con el servidor en tiempo real.",
//        "error"
//      );
//    }

//    // Escucha el botón "Enviar"
//    document
//      .getElementById("sendButton")
//      ?.addEventListener("click", async () => {
//        const bibEl = document.getElementById("bibNumber");
//        const distEl = document.getElementById("distance");
//        const bib = bibEl ? parseInt(bibEl.value) : NaN;
//        const dist = distEl ? parseFloat(distEl.value) : NaN;

//        if (isNaN(bib) || isNaN(dist)) {
//          this.showMessage("Completa los datos antes de enviar.", "error");
//          return;
//        }
//        await this.sendUpdate(raceId, bib, dist);
//      });
//  },

//  sendUpdate: async function (raceId, bibNumber, distanceKm) {
//    if (!this.connection) {
//      console.error("❌ No hay conexión activa con SignalR.");
//      this.showMessage("No conectado al servidor.", "error");
//      return;
//    }

//    try {
//      console.info(
//        `📤 Invocando UpdateRaceStatus (race=${raceId}, bib=${bibNumber}, dist=${distanceKm})`
//      );
//      await this.connection.invoke(
//        "UpdateRaceStatus",
//        raceId,
//        bibNumber,
//        distanceKm
//      );
//      console.log("📤 Invocación enviada correctamente.");
//    } catch (err) {
//      console.error("❌ Error al enviar:", err);
//      this.showMessage("Error al enviar actualización.", "error");
//    }
//  },

//  renderUpdate: function (update) {
//    const list = document.getElementById("race-updates");
//    if (!list) return;

//    // Normalizamos nombres (SignalR serializa a camelCase por defecto)
//    const bib = update.bibNumber ?? update.BibNumber ?? update.bibnumber ?? 0;
//    const dist =
//      update.distanceKm ?? update.DistanceKm ?? update.distancekm ?? 0;

//    const li = document.createElement("li");
//    li.textContent = `Dorsal: ${bib} — Distancia: ${dist} km`;
//    list.appendChild(li);
//  },

//  showMessage: function (text, type = "info") {
//    const el = document.getElementById("race-message");
//    if (!el) return;
//    el.textContent = text;
//    el.className = `message ${type} p-2 rounded mb-2`;
//  },

//  stop: async function () {
//    if (this.connection) {
//      await this.connection.stop();
//      this.connection = null;
//      console.log("🔴 Conexión SignalR cerrada");
//    }
//  },
//};

// export function initDotNetListener(dotnetObj) {
//     const connection = new signalR.HubConnectionBuilder()
//         .withUrl("/race-hub")
//         .build();

//     connection.on("ReceiveRaceUpdate", msg => {
//         dotnetObj.invokeMethodAsync("ReceiveFromHub", msg);
//     });

//     connection.start();
// }
