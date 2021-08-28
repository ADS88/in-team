import CoursePage from "../components/courses/CoursePage"
import CoursesPage from "../components/courses/CoursesPage"
import TeamPage from "../components/courses/TeamPage"
import Dashboard from "../components/dashboard/Dashboard"
import AlphasPage from "../components/alphas/AlphasPage"
import AlphaPage from "../components/alphas/AlphaPage"
import IRoute from "./IRoute"
import StatePage from "../components/alphas/StatePage"
import SurveyPage from "../components/surveys/SurveyPage"
import CreateSurveyPage from "../components/surveys/CreateSurveyPage"
import AnswerSurveyPage from "../components/surveys/AnswerSurveyPage"
import ProfilePage from "../components/profile/ProfilePage"
import LeaderboardPage from "../components/leaderboard/LeaderboardPage"

const authenticatedRoutes: IRoute[] = [
  {
    path: "/course/:id",
    name: "Single course page",
    exact: true,
    component: CoursePage,
  },
  {
    path: "/team/:id",
    name: "Team page",
    exact: true,
    component: TeamPage,
  },
  {
    path: "/courses",
    name: "Courses page",
    exact: true,
    component: CoursesPage,
  },
  {
    path: "/",
    name: "Dashboard page",
    exact: true,
    component: Dashboard,
  },
  {
    path: "/alphas",
    name: "Alpha page",
    exact: true,
    component: AlphasPage,
  },
  {
    path: "/alpha/:id",
    name: "Team page",
    exact: true,
    component: AlphaPage,
  },
  {
    path: "/state/:id",
    name: "State page",
    exact: true,
    component: StatePage,
  },
  {
    path: "/surveys",
    name: "Surveys page",
    exact: true,
    component: SurveyPage,
  },
  {
    path: "/createsurvey",
    name: "Create Survey page",
    exact: true,
    component: CreateSurveyPage,
  },
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
    path: "/leaderboard",
    name: "Leaderboard Page",
    exact: true,
    component: LeaderboardPage,
  },
]

export default authenticatedRoutes
