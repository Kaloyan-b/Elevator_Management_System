
# Elevator Management System

A C# application for simulating a multi-elevator management system, designed to handle dynamic elevator movements, call requests, and optimal distribution across floors.

---

## 🚀 Features

- **Asynchronous Request Handling**: Smooth processing of concurrent elevator calls.
- **Dynamic Elevator Distribution**: Automatically spreads elevators across floors to maximize efficiency.
- **Realistic Simulations**: Simulates elevator movement with delays and real-time updates.

---

## 🛠️ Technologies Used

- **Language**: C#
- **Framework**: .NET Core
- **Concurrency**: `Task` and `ConcurrentQueue` for asynchronous operations

---

## 📜 How It Works

### Main Components:

1. **Elevator Class**:
   - Tracks the elevator’s current floor, movement status, and ID.
   - Handles movement to the target floor with real-time updates.

   ```csharp
   public async Task Move(int floor)
   {
       Console.WriteLine($"Elevator {Id} moving to floor {floor}...");
       await Task.Delay(1000); // Simulate delay
       Floor = floor;
       Console.WriteLine($"Elevator {Id} arrived at floor {floor}.");
   }
   ```

2. **Request Processing**:
   - Uses a `ConcurrentQueue` to enqueue and handle requests asynchronously.
   - Selects the best elevator based on proximity to the request source.

   ```csharp
   async Task ProcessRequests()
   {
       while (true)
       {
           if (requests.TryDequeue(out var request))
           {
               Console.WriteLine($"Processing request from {request.SourceFloor} to {request.DestinationFloor}");
               // Assign the best elevator and handle the request.
           }
       }
   }
   ```

3. **SpreadOut Logic**:
   - Automatically distributes elevators evenly across floors when idle.

   ```csharp
   async Task SpreadOut()
   {
       int spread = maxFloor / elevators.Count;
       for (int i = 0; i < elevators.Count; i++)
       {
           int target = i * spread;
           await elevators[i].Move(target);
       }
   }
   ```

---

## 🧪 Usage Instructions
4. Use the following commands to interact with the system:
   - **Move an Elevator**: `move {elevator_id} {floor}`
   - **Call an Elevator**: `call {current_floor} {destination_floor}`
   - **Distribute Elevators**: `spread`
   - **Stop the Program**: `stop`

---

## 📊 Example Session

```plaintext
TO MOVE A CABIN, USE COMMAND: move {elevator id} {floor to move to}
TO SIMULATE AN ELEVATOR CALL, USE COMMAND: call {floor calling from} {floor you want to go to}

> call 3 7
Request added: From Floor 3 to Floor 7
Processing request from 3 to 7
Elevator(1) is moving to floor 3...
Elevator(1) is moving to floor 7...

> spread
Spreading out Elevator(0) to floor 0
Spreading out Elevator(1) to floor 3
Spreading out Elevator(2) to floor 6

---

## 📝 License

This project is licensed under the MIT License. See the `LICENSE` file for details.

---
