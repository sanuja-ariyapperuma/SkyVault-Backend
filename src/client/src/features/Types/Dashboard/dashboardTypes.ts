export type OptionsType = {
  value: string;
  label: string;
};

export type ResultProfiles = {
  lastName: string;
  otherNames: string;
  passportNumber: string;
  profileId: string;
  salutation: string;
};

export type SearchResponse = {
  query: string;
  profiles: ResultProfiles[];
};
