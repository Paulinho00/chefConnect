export type Reservation = {
  id: string;
  approved: boolean;
  approvingWorkerId: string;
  date: string;
  deleted: boolean;
  numberOfPeople: number;
  reservationStatus: string;
  restaurantId: string;
  userId: string;
};
