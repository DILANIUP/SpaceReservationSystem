namespace SpaceReservationSystem.Domain.Enums;

public enum RoleCode
{
    Student = 1,
    Teacher = 2,
    Coordinator = 3, // REcibe la solicitud de student y teacher
    Vicerrector = 4, // aprueba/niega la solicitud generada
    Bienes = 5, // asigna espacio
    Admin = 6   // Gestiona el sistema
}

public enum ReservationStatus
{
    Draft = 1, // Solicitud  
    PendingCoordinator = 2, // Solicitud pendiente de aprobación
    PendingVicerrector = 3, // Solicitud pendiente de aprobación
    PendingAssets = 4, // Solicitud pendiente de aprobación
    Approved = 5, // Solicitud aprobada
    Rejected = 6, // Solicitud rechazada
    Cancelled = 7, // Solicitud cancelada por el usuario
}

public enum SpaceType
{
    Classroom = 1,
    Laboratory = 2,
    Auditorium = 3,
    Cafeteria = 4,
    Other = 5
}

public enum AlertType
{
    Damage = 1,
    Unavailable = 2,
    Maintenance = 3,
    Incident = 4,
    Other = 5
}