export type PassportType = {
  id: string;
  salutationId: string;
  lastName: string;
  otherNames: string;
  nationalityId: string;
  genderId: string;
  placeOfBirth: string;
  passportNumber: string;
  dateOfBirth: Date | null;
  passportExpiryDate: Date | null;
  countryId: string;
};
