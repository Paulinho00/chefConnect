resource "aws_sqs_queue" "analytic_event_queue" {
  name = "analytic_event_queue"
}

output "analytics_event_queue_url" {
  value = aws_sqs_queue.analytic_event_queue.url
}

output "analytics_event_queue_id" {
  value = aws_sqs_queue.analytic_event_queue.id
}