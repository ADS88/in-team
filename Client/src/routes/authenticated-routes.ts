import IRoute from "./IRoute"
import AnswerSurveyPage from "../components/surveys/AnswerSurveyPage"
import ProfilePage from "../components/profile/ProfilePage"
import LeaderboardPage from "../components/leaderboard/LeaderboardPage"

//All routes within the application that require authentication to access
const authenticatedRoutes: IRoute[] = [
  {
    path: "/answersurvey/:id",
    name: "Answer Survey page",
    exact: true,
    component: AnswerSurveyPage,
  },
  {
    path: "/profile",
    name: "User Profile Page",
    exact: true,
    component: ProfilePage,
  },
  {
    path: "/profile/:id",
    name: "User Profile Page",
    exact: true,
    component: ProfilePage,
  },
  {
    path: "/leaderboard",
    name: "Leaderboard Page",
    exact: true,
    component: LeaderboardPage,
  },
]

export default authenticatedRoutes
