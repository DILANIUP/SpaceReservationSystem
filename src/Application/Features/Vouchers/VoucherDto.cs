namespace SpaceReservationSystem.Application.Features.Vouchers;

public sealed record GenerateVoucherRequest(Guid ReservationId);
public sealed record VoucherResponse(
    Guid Id,
    string PdfFilePath,
    DateTime GenerationDate
);

public sealed record VoucherFileResult(
    byte[] Bytes,
    string FileName
);